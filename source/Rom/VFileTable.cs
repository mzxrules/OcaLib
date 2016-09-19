using mzxrules.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mzxrules.OcaLib
{
    public class VFileTable : AbstractVFileTable, /*IDisposable,*/ IEnumerable<FileRecord>
    {
        protected Dictionary<long, FileRecord> DmaTable => DmaFile.Table;
        protected DmaData DmaFile;
        public RomVersion Version { get; protected set; }

        protected string RomLocation;

        byte[] CachedFile;
        FileAddress CachedFileAddress;

        //"Code" file metadata for file lookups
        protected class FileRefTable
        {
            public string StartKey { get; }
            public int RecSize { get; }
            public int FileStartOff { get; }
            public int FileEndOff { get; }

            public FileRefTable()
            {
                StartKey = null;
            }

            public FileRefTable (string key, int size, int off)
            {
                StartKey = key;
                RecSize = size;
                FileStartOff = off;
                FileEndOff = off + 4;
            }
        }

        protected FileRefTable SceneTable = new FileRefTable();
        protected FileRefTable TitleCardTable = new FileRefTable();
        protected FileRefTable HyruleSkyboxTable = new FileRefTable();

        FileRefTable ActorTable = new FileRefTable("ActorTable_Start", 0x20, 0);
        FileRefTable ObjectTable = new FileRefTable("ObjectTable_Start", 0x08, 0);
        FileRefTable ParticleTable = new FileRefTable("ParticleTable_Start", 0x1C, 0);
        FileRefTable GameContextTable = new FileRefTable("GameContextTable_Start", 0x30, 4);
        FileRefTable PlayerPauseTable = new FileRefTable("PlayerPauseOverlayTable_Start", 0x1C, 4);


        protected VFileTable() { }

        public override RomFile GetSceneFile(int i)
        {
            throw new InvalidOperationException();
        }

        public override FileAddress GetDmaDataStart()
        {
            return DmaFile.Address.VirtualAddress;
        }

        private void ResetFileCache()
        {
            CachedFile = null;
            CachedFileAddress = new FileAddress();
        }

        #region GetFile

        /// <summary>
        /// Returns a stream pointed to the decompressed file at the given address
        /// </summary>
        /// <param name="virtualAddress"></param>
        /// <returns></returns>
        public RomFile GetFile(FileAddress virtualAddress)
        {
            FileRecord record, temp;

            temp = new FileRecord(virtualAddress, new FileAddress(virtualAddress.Start, 0), -1);

            if (DmaTable.TryGetValue(virtualAddress.Start, out record))
            {
                if (virtualAddress.Size != record.VirtualAddress.Size)
                    record = temp;
            }
            else
                record = temp;
            return GetFile(record);
        }

        /// <summary>
        /// Returns a stream pointed to the decompressed file at the given address
        /// </summary>
        /// <param name="virtualAddress"></param>
        /// <param name="recordCopy"></param>
        /// <returns></returns>
        public RomFile GetFile(long virtualAddress)
        {
            FileRecord record; //file we're looking up

            if (!DmaTable.TryGetValue(virtualAddress, out record))
                throw new Exception();

            return GetFile(new FileRecord(record));
        }

        /// <summary>
        /// Returns a reference to a decompressed file
        /// </summary>
        /// <param name="record">The DMA address used to reference the file</param>
        /// <returns></returns>
        protected RomFile GetFile(FileRecord record)
        {
            MemoryStream ms;
            byte[] data;
            byte[] decompressedData;

            if (record.VirtualAddress == CachedFileAddress)
            {
                ms = new MemoryStream(CachedFile);
                return new RomFile(record, ms, Version);
            }

            using (FileStream fs = new FileStream(RomLocation, FileMode.Open, FileAccess.Read))
            {
                data = new byte[record.DataAddress.Size];
                fs.Position = record.DataAddress.Start;
                fs.Read(data, 0, (int)record.DataAddress.Size);

                if (record.IsCompressed)
                {
                    ms = new MemoryStream(data);
                    decompressedData = Yaz0.Decode(ms, (int)(record.DataAddress.Size));
                }
                else
                {
                    decompressedData = data;
                }
            }
            CachedFile = decompressedData;
            ms = new MemoryStream(decompressedData);
            CachedFileAddress = record.VirtualAddress;
            return new RomFile(record, ms, Version);
        }


        /// <summary>
        /// Returns a stream pointed to the decompressed file at the given address
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public RomFile GetFile(RomFileToken file) => GetFile(Addresser.GetRom(file, Version, "__Start"));
        
        #endregion

        /// <summary>
        /// Returns a file without attempting to decompress it.  
        /// </summary>
        /// <param name="virtualAddress">Address containing the location of the file</param>
        /// <returns>Returns a stream containing the file</returns>
        public Stream GetPhysicalFile(FileAddress virtualAddress) => GetPhysicalFile(virtualAddress.Start);
        
        /// <summary>
        /// Returns a file without attempting to decompress it.
        /// </summary>
        /// <param name="virtualAddress">Address containing the location of the file</param>
        /// <returns></returns>
        public Stream GetPhysicalFile(long virtualAddress)
        {
            MemoryStream ms;
            byte[] data;
            FileRecord tableRecord; //file we're looking up

            if (!DmaTable.TryGetValue(virtualAddress, out tableRecord))
                throw new Exception();

            using (FileStream fs = new FileStream(RomLocation, FileMode.Open, FileAccess.Read))
            {
                data = new byte[tableRecord.DataAddress.Size];
                fs.Position = tableRecord.DataAddress.Start;
                fs.Read(data, 0, (int)tableRecord.DataAddress.Size);

            }
            return ms = new MemoryStream(data);
        }


        /// <summary>
        /// Returns the full FileRecord for a file that contains the given address
        /// </summary>
        /// <param name="virtualAddress"></param>
        /// <returns>The FileRecord of the containing file, or null if no file contains the given address</returns>
        public FileRecord GetFileStart(long virtualAddress)
        {
            return DmaTable.FirstOrDefault(x => x.Value.VirtualAddress.Start <= virtualAddress
                && virtualAddress < x.Value.VirtualAddress.End).Value;
        }

        /// <summary>
        /// Tries to get the full FileRecord for a file with the given virtual FileAddress
        /// </summary>
        /// <param name="virtualAddress">The virtual FileAddress of the record to return. Does not verify end address</param>
        /// <param name="record">Returns the full FileRecord for that file</param>
        /// <returns>True if operation is successful</returns>
        public bool TryGetFileRecord(FileAddress virtualAddress, out FileRecord record)
        {
            return DmaTable.TryGetValue(virtualAddress.Start, out record);
        }

        /// <summary>
        /// Tries to get the full FileRecord for a file with the given starting virtual address
        /// </summary>
        /// <param name="virtualAddress">The virtual address of the record to return.</param>
        /// <param name="record">Returns the full FileRecord for that file</param>
        /// <returns>True if operation is successful</returns>
        public bool TryGetFileRecord(long virtualAddress, out FileRecord record)
        {
            return DmaTable.TryGetValue(virtualAddress, out record);
        }

        /// <summary>
        /// Tries to return the virtual FileAddress of a file with a given start address
        /// </summary>
        /// <param name="address">The file's VROM address. Must match FileAddress's Vrom.Start</param>
        /// <param name="resultAddress">The returned FileAddress</param>
        /// <returns>True if the operation is successful</returns>
        public bool TryGetVirtualAddress(long address, out FileAddress resultAddress)
        {
            FileRecord record;
            bool getValue;

            getValue = DmaTable.TryGetValue(address, out record);
            if (getValue)
                resultAddress = record.VirtualAddress;
            else
                resultAddress = new FileAddress();
            return getValue;
        }

        /// <summary>
        /// Gets the virtual FileAddress of a file with a given start address
        /// </summary>
        /// <param name="address">The file's VROM address. Must match FileAddress's Vrom.Start</param>
        /// <returns>The returned FileAddress</returns>
        public FileAddress GetVRomAddress(long address)
        {
            FileRecord record;

            if (!DmaTable.TryGetValue(address, out record))
                throw new FileNotFoundException();

            return record.VirtualAddress;
        }

        protected int ReadInt32(int addr)
        {
            FileRecord record;
            record = GetFileStart(addr);
            using (BinaryReader reader = new BinaryReader(GetFile(record)))
            {
                reader.BaseStream.Position = record.GetRelativeAddress(addr);
                return reader.ReadBigInt32();
            }
        }


        public void Dispose()
        {
        }
        

        public IEnumerator<FileRecord> GetEnumerator()
        {
            return DmaTable.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return DmaTable.Values.GetEnumerator();
        }


        #region FetchAddresses

        public FileAddress GetSceneVirtualAddress(int scene)
        {
            return GetFileByRomTable(SceneTable, scene);
        }


        public FileAddress GetActorVirtualAddress(int actor)
        {
            return GetFileByRomTable(ActorTable, actor);
        }

        public FileAddress GetObjectVirtualAddress(int obj)
        {
            //oot obj 0x00E3, 0x00FB malformed references
            return GetFileByRomTable(ObjectTable, obj);
        }

        public FileAddress GetParticleEffectAddress(int index)
        {
            return GetFileByRomTable(ParticleTable, index);
        }

        public FileAddress GetGameContextAddress(int i)
        {
            return GetFileByRomTable(GameContextTable, i);
        }

        public FileAddress GetPlayPauseAddress(int i)
        {
            return GetFileByRomTable(PlayerPauseTable , i);
        }

        protected FileAddress GetFileByRomTable(FileRefTable refTable, int index)
        {
            string table = refTable.StartKey;
            int size = refTable.RecSize;
            int offset = refTable.FileStartOff;
            int recordAddr, startAddr, endAddr;
            RomFileToken token = ORom.FileList.invalid;
            if (Version.Game == Game.OcarinaOfTime)
                token = ORom.FileList.code;
            if (Version.Game == Game.MajorasMask)
                token = MRom.FileList.code;

            recordAddr = Addresser.GetRom(token, Version, table);
            startAddr = recordAddr + (index * size) + offset;
            startAddr = ReadInt32(startAddr);
            endAddr = recordAddr + (index * size) + refTable.FileEndOff;
            endAddr = ReadInt32(endAddr);
            FileAddress result = new FileAddress();
            try
            {
                result = GetVRomAddress(startAddr);
            }
            catch
            {
                result = new FileAddress(startAddr, endAddr);
            }

            return result;
        }
        
        #endregion



        #region GetOverlayRecord
        public ActorOverlayRecord GetActorOverlayRecord(int actor)
        {
            int addr;
            RomFileToken token = ORom.FileList.invalid;
            if (Version.Game == Game.OcarinaOfTime)
                token = ORom.FileList.code;
            if (Version.Game == Game.MajorasMask)
                token = MRom.FileList.code;

            RomFile code = GetFile(token);
            if (!Addresser.TryGetRom(token, Version, ActorTable.StartKey, out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (actor * 0x20));
            return new ActorOverlayRecord(actor, new BinaryReader(code));
        }

        public GameContextRecord GetGameContextRecord(int index)
        {
            int addr;
            RomFileToken token = ORom.FileList.invalid;
            if (Version.Game == Game.OcarinaOfTime)
                token = ORom.FileList.code;
            if (Version.Game == Game.MajorasMask)
                token = MRom.FileList.code;

            RomFile code = GetFile(token);
            if (!Addresser.TryGetRom(token, Version, GameContextTable.StartKey, out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (index * 0x30));
            return new GameContextRecord(index, new BinaryReader(code));
        }

        public ParticleEffectOverlayRecord GetParticleEffectOverlayRecord(int index)
        {
            int addr;
            RomFileToken token = ORom.FileList.invalid;
            if (Version.Game == Game.OcarinaOfTime)
                token = ORom.FileList.code;
            if (Version.Game == Game.MajorasMask)
                token = MRom.FileList.code;

            RomFile code = GetFile(token);
            if (!Addresser.TryGetRom(token, Version, ParticleTable.StartKey, out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (index * 0x1C));
            return new ParticleEffectOverlayRecord(index, new BinaryReader(code));
        }

        public PlayPauseOverlayRecord GetPlayPauseOverlayRecord(int index)
        {
            int addr;
            RomFileToken token = ORom.FileList.invalid;
            if (Version.Game == Game.OcarinaOfTime)
                token = ORom.FileList.code;
            if (Version.Game == Game.MajorasMask)
                token = MRom.FileList.code;

            RomFile code = GetFile(token);
            if (!Addresser.TryGetRom(token, Version, PlayerPauseTable.StartKey, out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (index * 0x1C));
            return new PlayPauseOverlayRecord(index, new BinaryReader(code));
        }

        #endregion
        
    }
}
