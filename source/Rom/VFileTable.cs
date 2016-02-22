using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mzxrules.OcaLib
{
    public class VFileTable : AbstractVFileTable, IDisposable, IEnumerable<FileRecord>
    {
        protected Dictionary<long, FileRecord> DmaTable { get { return dmaFile.Table; } }
        protected DmaData dmaFile;
        public RomVersion Version { get; protected set; }

        protected string romLoc;

        byte[] CachedFile;
        FileAddress CachedFileAddress;

        protected VFileTable() { }

        public override RomFile GetSceneFile(int i)
        {
            throw new InvalidOperationException();
        }

        public override FileAddress GetDmaDataStart()
        {
            return dmaFile.Address.VirtualAddress;
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
            return GetFile(virtualAddress.Start);
        }

        /// <summary>
        /// Returns a stream pointed to the decompressed file at the given address
        /// </summary>
        /// <param name="virtualAddress"></param>
        /// <param name="recordCopy"></param>
        /// <returns></returns>
        public RomFile GetFile(long virtualAddress)
        {
            FileRecord record;            //file we're looking up

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

            using (FileStream fs = new FileStream(romLoc, FileMode.Open, FileAccess.Read))
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
        public RomFile GetFile(RomFileToken file)
        {
            return GetFile(Addresser.GetRom(file, Version, "__Start"));
        }

        #endregion

        /// <summary>
        /// Returns a file without attempting to decompress it.  
        /// </summary>
        /// <param name="virtualAddress">Address containing the location of the file</param>
        /// <returns>Returns a stream containing the file</returns>
        public Stream GetPhysicalFile(FileAddress virtualAddress)
        {
            return GetPhysicalFile(virtualAddress.Start);
        }

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

            using (FileStream fs = new FileStream(romLoc, FileMode.Open, FileAccess.Read))
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
            return DmaTable.SingleOrDefault(x => x.Value.VirtualAddress.Start <= virtualAddress
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
    }
}
