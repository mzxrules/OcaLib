using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using mzxrules.Helper;

namespace mzxrules.OcaLib.Elf
{
    public class ElfUtil
    {
        static string[] ovlSectionsStr = { ".text", ".data", ".rodata", ".bss" };
        
        class Ovl
        {
            public N64Ptr start;
            public N64Ptr header;
            public N64Ptr pEnd;
            public N64Ptr vEnd;

            public Dictionary<string, string> BindingSymbolName = new Dictionary<string, string>();
            public Dictionary<string, N64Ptr> BindingValue = new Dictionary<string, N64Ptr>();

            public void UpdateActorOverlay(BinaryWriter rom, int actor, uint dmadata, uint vromStart)
            {
                uint vromEnd = vromStart + (uint)(pEnd - start);
                //update dmadata
                rom.BaseStream.Position = dmadata;
                rom.WriteBig(vromStart);
                rom.WriteBig(vromEnd);
                rom.WriteBig(vromStart);
                rom.WriteBig(0);

                rom.BaseStream.Position = 0xB5E490 + (actor * 0x20);

                rom.WriteBig(vromStart);
                rom.WriteBig(vromEnd);
                rom.WriteBig(start);
                rom.WriteBig(vEnd);
                rom.Seek(4, SeekOrigin.Current);
                rom.WriteBig(BindingValue["init"]);
            }
        }

        public static bool TryConvertToOverlay(BinaryReader elf, string path, N64Ptr rpoint)
        {
            Ovl odata = new Ovl
            {
                start = rpoint,
                BindingSymbolName = { {"init", "initVars" } }
            };
            bool result = TryConvertToOverlay(elf, odata, out byte[] ovlByte);

            string rompath = @"C:\Users\mzxrules\Documents\Roms\N64\Games\zcodec\ovl_test.z64";

            //custom inject
            using (FileStream rom = new FileStream(rompath, FileMode.Open))
            {
                odata.UpdateActorOverlay(new BinaryWriter(rom), 0xD8, 0xD280, 0x347E000);
                rom.Position = 0x347E000;
                rom.Write(ovlByte, 0, ovlByte.Length);
                CRC.Write(rom);
            }
            
            //write the overlay for debugging purposes
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(ovlByte, 0, ovlByte.Length);
            }

            
            return result;
        }
        private static bool TryConvertToOverlay(BinaryReader elf, Ovl odata, out byte[] ovlByte)
        {
            ovlByte = new byte[0];
            FileHeader header = new FileHeader(elf);
            if (!header.VerifyMagic())
            {
                Console.WriteLine("Not an elf, or not a supported elf format");
                return false;
            }

            //Extract section headers
            List<SectionHeader> sections = GetSections(elf, header);

            //build memory map
            int relocations = GetRelocations(sections);

            Dictionary<string, SectionHeader> map = new Dictionary<string, SectionHeader>();
            Dictionary<string, (N64Ptr addr, int size)> newMap = new Dictionary<string, (N64Ptr addr, int size)>();
            
            N64Ptr cur = odata.start;

            for (int i = 0; i < ovlSectionsStr.Length; i++)
            {
                var secName = ovlSectionsStr[i];
                if (secName == ".bss")
                {
                    odata.header = cur - odata.start;
                    cur += 0x14 + (relocations * 4) + 4; //header size, reloction size, header offset
                    cur = Align.To16(cur );
                    odata.pEnd = cur;
                }
                N64Ptr localStart = cur;
                foreach (var item in sections.Where(x => x.Name.StartsWith(secName)))
                {
                    map[item.Name] = item;
                    item.NS = new SectionHelper(item, cur, cur - odata.start, i + 1, cur - localStart);
                    cur = Align.To16(cur + item.sh_size);
                }
                if (secName == ".bss")
                {
                    odata.vEnd = cur;
                }
                int size = cur - localStart;
                newMap[secName] = (localStart, size);
            }

            //get relocated symtables
            Dictionary<int, List<Elf32_Sym>> symtabs = GetSymbols(elf, sections);

            //create our file
            ovlByte = new byte[odata.pEnd - odata.start];
            List<Overlay.RelocationWord> ovlRelocations = new List<Overlay.RelocationWord>();
            using (BinaryWriter ovl = new BinaryWriter(new MemoryStream(ovlByte)))
            {
                //copy data into the overlay
                foreach (var kv in map.OrderBy(x => x.Value.NS.Addr))
                {
                    var (sec, ptr, fOff, newSecId, sOff) = kv.Value.NS;
                    if (sec == null)
                        continue;

                    if (sec.sh_type == SectionHeader.SHT_.PROGBITS)
                    {
                        ovl.BaseStream.Position = fOff;
                        elf.BaseStream.Position = sec.sh_offset;
                        byte[] data = elf.ReadBytes(sec.sh_size);
                        ovl.Write(data);
                    }
                }

                //handle relocations
                foreach (var section in sections.Where(x => x.sh_type == SectionHeader.SHT_.REL))
                {
                    List<Elf32_Rel> rel = GetSectionRelocations(elf, section);
                    var secInfo = sections[section.sh_info];
                    if (secInfo.Name == ".pdr")
                        continue;
                    var (sec, ptr, fOff, newSecId, sOff) = secInfo.NS; //Sets section that REL applies to
                    var symtab = symtabs[section.sh_link];

                    for (int i = 0; i < rel.Count; i++)
                    {
                        var relcur = rel[i];
                        var symbol = symtab[relcur.R_Sym];
                        if (symbol.st_shndx == 0)
                        {
                            Console.WriteLine($"{symbol.Name}: Undefined Section");
                            continue;
                        }

                        N64Ptr relSecAddr = sections[symbol.st_shndx].NS.Addr;
                        switch (relcur.R_Type)
                        {
                            case Reloc.R_MIPS32:
                                {
                                    ovl.BaseStream.Position = fOff + relcur.r_offset;
                                    elf.BaseStream.Position = sec.sh_offset + relcur.r_offset;
                                    var data = elf.ReadBigInt32() + relSecAddr + symbol.st_value;
                                    ovl.WriteBig(data);
                                    ovlRelocations.Add(new Overlay.RelocationWord((Overlay.Section)newSecId, Reloc.R_MIPS32, sOff + relcur.r_offset));
                                    break;
                                }
                            case Reloc.R_MIPS26:
                                {
                                    ovl.BaseStream.Position = fOff + relcur.r_offset;
                                    elf.BaseStream.Position = sec.sh_offset + relcur.r_offset;
                                    var data = elf.ReadBigUInt32();
                                    var newAddrDat = (uint)((relSecAddr + symbol.st_value >> 2) & 0x3FFFFFF);
                                    data += newAddrDat;
                                    ovl.WriteBig(data);
                                    ovlRelocations.Add(new Overlay.RelocationWord((Overlay.Section)newSecId, Reloc.R_MIPS26, sOff + relcur.r_offset));
                                    break;
                                }
                            case Reloc.R_MIPS_HI16:
                                {
                                    if (!(i + 1 < rel.Count))
                                    {
                                        Console.WriteLine("R_MIPS_HI16 without a proper pair");
                                        return false;
                                    }
                                    i++;
                                    var relLo = rel[i];
                                    elf.BaseStream.Position = sec.sh_offset + relcur.r_offset + 2;
                                    N64Ptr addr = elf.ReadBigUInt16() << 16;
                                    elf.BaseStream.Position = sec.sh_offset + relLo.r_offset;
                                    var loData = elf.ReadBigUInt32();
                                    addr += loData & 0xFFFF;
                                    addr += relSecAddr;

                                    var hi16 = (ushort)((addr >> 16));
                                    var lo16 = (ushort)(addr & 0xFFFF);

                                    bool isOri = ((loData >> 26) & 0x3F) == 0x0D;
                                    if ((addr & 0xFFFF) > 0x8000 && !isOri)
                                    {
                                        hi16++;
                                    }

                                    ovl.BaseStream.Position = fOff + relcur.r_offset + 2;
                                    ovl.WriteBig(hi16);

                                    ovl.BaseStream.Position = fOff + relLo.r_offset + 2;
                                    ovl.WriteBig(lo16);

                                    ovlRelocations.Add(new Overlay.RelocationWord((Overlay.Section)newSecId, Reloc.R_MIPS_HI16, sOff + relcur.r_offset));
                                    ovlRelocations.Add(new Overlay.RelocationWord((Overlay.Section)newSecId, Reloc.R_MIPS_LO16, sOff + relLo.r_offset));
                                    break;
                                }
                            case Reloc.R_MIPS_LO16:
                                {
                                    Console.WriteLine("R_MIPS_LO16 without a proper pair");
                                    return false;
                                }
                            default:
                                {
                                    Console.WriteLine("Incompatible R_MIPS type, cannot convert");
                                    return false;
                                }
                        }
                    }
                }

                ovl.BaseStream.Position = odata.header;

                foreach (var name in ovlSectionsStr)
                    ovl.WriteBig(newMap[name].size);
                ovl.WriteBig(relocations);

                foreach (var item in ovlRelocations)
                {
                    ovl.WriteBig(item.Word);
                }
                ovl.BaseStream.Position = ovl.BaseStream.Length - 4;
                var seekback = (int)(ovl.BaseStream.Length - odata.header);
                ovl.WriteBig(seekback);
            }

            BindRelocatedSymbols(odata, sections, symtabs);

            return true;
        }

        private static void BindRelocatedSymbols(Ovl odata, List<SectionHeader> sections, Dictionary<int, List<Elf32_Sym>> symtabs)
        {
            var list = symtabs.Values.SelectMany(x => x);

            foreach(var kv in odata.BindingSymbolName)
            {
                var symbol = list.Where(x => x.Name == kv.Value).SingleOrDefault();


                if (symbol.st_shndx > 0 && symbol.st_shndx < sections.Count)
                {
                    var section = sections[symbol.st_shndx];
                    if (section.NS == null)
                    {
                        Console.WriteLine($"Binding {kv.Key}<-{kv.Value} failed");
                        continue;
                    }

                    odata.BindingValue[kv.Key] = symbol.st_value + section.NS.Addr;
                }
            }
        }

        private static Dictionary<int, List<Elf32_Sym>> GetSymbols(BinaryReader elf, List<SectionHeader> sections)
        {
            Dictionary<int, List<Elf32_Sym>> symtabs = new Dictionary<int, List<Elf32_Sym>>();

            foreach (var section in sections.Where(x => x.sh_type == SectionHeader.SHT_.REL))
            {
                var secInfo = sections[section.sh_info];
                if (secInfo.Name == ".pdr")
                    continue;

                var sh_link = section.sh_link;
                if (!symtabs.ContainsKey(sh_link))
                {
                    symtabs.Add(sh_link, InitializeSymtab(elf, sections, sh_link));
                }
            }

            return symtabs;
        }

        private static List<SectionHeader> GetSections(BinaryReader elf, FileHeader header)
        {
            List<SectionHeader> sections = new List<SectionHeader>();
            elf.BaseStream.Position = header.e_shoff;
            for (int i = 0; i < header.e_shnum; i++)
            {
                var section = new SectionHeader(elf);
                sections.Add(section);
            }
            
            //set section names
            var shstr = sections[header.e_shstrndx];

            elf.BaseStream.Position = shstr.sh_offset;
            MemoryStream ms = new MemoryStream(elf.ReadBytes(shstr.sh_size));
            foreach (var section in sections)
            {
                ms.Position = section.sh_name;
                section.Name = CStr.Get(ms);
            }

            return sections;
        }

        private static int GetRelocations(List<SectionHeader> sections)
        {
            int relocations = 0;
            foreach (var section in sections.Where(x => x.sh_type == SectionHeader.SHT_.REL))
            {
                var secInfo = sections[section.sh_info];
                if (secInfo.Name == ".pdr")
                    continue;
                
                var entries = section.sh_size / section.sh_entsize;
                relocations += entries;
            }

            return relocations;
        }

        private static List<Elf32_Sym> InitializeSymtab(BinaryReader elf, List<SectionHeader> sections, int symtabId)
        {
            SectionHeader symtab = sections[symtabId];
            SectionHeader strtab = sections[symtab.sh_link];

            List<Elf32_Sym> symbols = new List<Elf32_Sym>();
            var elements = symtab.sh_size / symtab.sh_entsize;
            elf.BaseStream.Position = symtab.sh_offset;
            for (int i = 0; i < elements; i++)
            {
                var symbol = new Elf32_Sym(elf);
                symbols.Add(symbol);
            }

            elf.BaseStream.Position = strtab.sh_offset;
            MemoryStream ms = new MemoryStream(elf.ReadBytes(strtab.sh_size));
            foreach (var symbol in symbols)
            {
                ms.Position = symbol.st_name;
                symbol.Name = CStr.Get(ms);
                
                //if (symbol.st_shndx > 0 && symbol.st_shndx < sections.Count)
                //{
                //    var section = sections[symbol.st_shndx];
                //    if (section.NS == null)
                //        continue;

                //    symbol.st_value += section.NS.Addr;
                //    symbol.st_shndx = Elf32_Sym.ABS;
                //}

            }
            return symbols;
        }
        
        private static List<Elf32_Rel> GetSectionRelocations(BinaryReader elf, SectionHeader section)
        {
            List<Elf32_Rel> rel = new List<Elf32_Rel>();

            elf.BaseStream.Position = section.sh_offset;
            for (int i = 0; i < section.sh_size / section.sh_entsize; i++)
            {
                rel.Add(new Elf32_Rel(elf));
            }
            return rel;
        }
    }
}
