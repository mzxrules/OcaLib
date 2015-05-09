using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using mzxrules.OcaLib.Addr2;
using Domain = mzxrules.OcaLib.Addr2.SpaceDomain;
using Addr = mzxrules.OcaLib.Addr2.Address;


namespace mzxrules.OcaLib
{
    public static class Addresser
    {
        static Addresses AddressDoc { get; set; }
        static Addresser()
        {
            var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("mzxrules.OcaLib.Addresses.xml");


            XmlSerializer serializer;

            serializer = new XmlSerializer(typeof(Addresses));

            using (XmlReader reader = XmlReader.Create(stream))
            {
                AddressDoc = (Addresses)serializer.Deserialize(reader);
            }
        }

        public static bool TryConvertToRam(RomFileToken file, RomVersion version, string addrVar, out int v)
        {
            var block = GetBlock(version, file.ToString());

            if (TryMagicConverter(block, addrVar, version, Domain.RAM, out v))
                return true;

            return false;
        }

        public static bool TryConvertToRom(RomFileToken file, RomVersion version, uint ramAddr, out int v)
        {
            
            throw new NotImplementedException();
            //int romStart;
            //int ramStart;
            //if (!TryGetAddress(file, version, "__Start", out romStart)
            //    || !TryGetAddress("ram", version, "CONV_" + file, out ramStart)
            //    || ramAddr < ramStart)
            //{
            //    v = 0;
            //    return false;
            //}
            //ramAddr &= 0xFFFFFF;
            //v = romStart + (int)ramAddr - ramStart;
            //return true;
        }

        public static bool TryGetRam(RomVersion version, string addrVar, out int v)
        {
            Block block;
            v = 0;
            if (!TryGetBlock(version, addrVar, out block))
                return false;

            if (!TryMagicConverter(block, addrVar, version, Domain.RAM, out v))
                return false;

            return true;

            //return TryGetAddress("ram", version, addrVar, out v);
        }

        public static bool TryGetRom(RomFileToken file, RomVersion version, string addrVar, out int v)
        {
            var block = GetBlock(version, file.ToString());

            if (TryMagicConverter(block, addrVar, version, Domain.ROM, out v))
                return true;

            return false;
        }

        public static int GetRom(RomFileToken file, RomVersion version, string addrVar)
        {
            int addr;

            var block = GetBlock(version, file.ToString());

            if (addrVar == "__Start")
                TryGetStart(block, version, Domain.ROM, out addr);
            else
                TryMagicConverter(block, addrVar, version, Domain.ROM, out addr);
            return addr;
        }


        private static bool TryGetStart(Block block, RomVersion version, Domain domain, out int start)
        {
            start = 0;
            var startAddr = block.Start.SingleOrDefault(x => x.domain == domain);

            if (startAddr == null
                || !TryGetAddrValue(startAddr, version, out start))
                return false;
            return true;
        }

        private static bool TryGetBlock(RomVersion version, string addressIdentifier, out Block block)
        {
            var game = (from a in AddressDoc.Game
                       where a.name.ToString() == version.Game.ToString()
                       select a).Single();

            block = (from b in game.Block
                     where b.Identifier.Any(x => x.id == addressIdentifier)
                     select b).SingleOrDefault();
            if (block != null)
                return true;

            return false;
        }

        private static Block GetBlock(RomVersion version, string blockId)
        {
            var game = from a in AddressDoc.Game
                       where a.name.ToString() == version.Game.ToString()
                       select a;

            var block = (from b in game.Single().Block
                         where b.name == blockId
                         select b).SingleOrDefault();
            return block;
        }

        private static bool TryMagicConverter(Block block, string ident, RomVersion version, Domain domain, out int result)
        {
            int start, lookup;
            result = 0;

            var lookupAddr = block.Identifier.SingleOrDefault(x => x.id == ident);

            if (lookupAddr == null)
                return false;

            if (lookupAddr.Item is Addr)
            {
                Addr addr = (Addr)lookupAddr.Item;

                if (!TryGetAddrValue(addr, version, out lookup))
                    return false;

                //if lookup is absolute, and in the same space, we have it

                if (addr.reftype == AddressType.absolute
                    && addr.domain == domain)
                {
                    result = lookup;
                    return true;
                }

                //if lookup is absolute, but not in the same space, convert to offset
                if (addr.reftype == AddressType.absolute && addr.domain != domain)
                {
                    int altStart;
                    Addr altStartAddr;

                    altStartAddr = block.Start.SingleOrDefault(x => x.domain == addr.domain);

                    if (altStartAddr == null
                        || !TryGetAddrValue(altStartAddr, version, out altStart))
                        return false;

                    lookup -= altStart;
                }
            }
            else //if (lookupAddr.Item is Offset)
            {
                Offset offset = (Offset)lookupAddr.Item;
                if (!TryGetOffsetValue(offset, out lookup))
                    return false;
            }

            if (!TryGetStart(block, version, domain, out start))
                return false;

            result = start + lookup;

            return true;
        }

        private static bool TryGetOffsetValue(Offset offset, out int result)
        {
            try
            {
                result = Convert.ToInt32(offset.Value, 16);
                return true;
            }
            catch
            {
                result = -1;
                return false;
            }
        }

        private static bool TryGetAddrValue(Addr2.Address addr, RomVersion version, out int result)
        {
            try
            {
                result = Convert.ToInt32(addr.Data.Single(x => x.ver == version.ToString()).Value, 16);
            }
            catch
            {
                result = -1;
                return false;
            }
            return true;
        }
    }
}
