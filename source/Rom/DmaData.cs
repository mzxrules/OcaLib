using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class FileRecord
    {
        //public const int LENGTH = 0x10;
        /// <summary>
        /// The file's mapped location in virtual space
        /// </summary>
        public FileAddress VirtualAddress { get; set; } 
        /// <summary>
        /// The file's mapped location on file. A zeroed end address means that the file is not compressed
        /// </summary>
        public FileAddress PhysicalAddress { get; set; } 
        /// <summary>
        /// The file's location independent of compression state
        /// </summary>
        public FileAddress DataAddress { get; private set; }
        public bool IsCompressed { get { return PhysicalAddress.End != 0; } }
        public int Index = -1;

        public FileRecord(FileRecord fileRecord)
        {
            VirtualAddress = fileRecord.VirtualAddress;
            PhysicalAddress = fileRecord.PhysicalAddress;
            DataAddress = fileRecord.DataAddress;
            Index = fileRecord.Index;
        }

        public FileRecord(FileAddress virtualAddr, FileAddress physicalAddr, int index)
        {
            VirtualAddress = virtualAddr;
            PhysicalAddress = physicalAddr;
            Index = index;

            if (IsCompressed)
            {
                DataAddress = new FileAddress(PhysicalAddress.Start, PhysicalAddress.End);
            }
            else
                DataAddress = new FileAddress(PhysicalAddress.Start, PhysicalAddress.Start + VirtualAddress.Size);
        }

        public long GetRelativeAddress(long offset)
        {
            return offset - VirtualAddress.Start;
        }
    }

    public class DmaData
    {
        public Dictionary<long, FileRecord> Table;
        public FileRecord Address { get; private set; }

        public DmaData (Stream s, ORom.Build version)
        {
            int address = Addresser.GetRom(ORom.FileList.dmadata, version, "__Start");
            Initialize(s, address);
        }

        public DmaData(Stream s, MRom.Build version)
        {
            int address = Addresser.GetRom(MRom.FileList.dmadata, version, "__Start");
            Initialize(s, address);
        }

        private void Initialize(Stream fs, int address)
        {
            FileRecord fileRecord;
            int length;         //length of file table
            int position;       //offset into rom of file table
            int positionEnd;    //end offset for the file table

            FileAddress fileVirtualAddress;
            FileAddress filePhysicalAddress;

            //initialize file table
            Table = new Dictionary<long, FileRecord>();


            BinaryReader br = new BinaryReader(fs);
            //set rom type dependent values
            position = address;

            //set stream position
            fs.Position = position;

            positionEnd = GetEndAddress(br);

            length = (positionEnd - position) / 0x10 + 1;

            for (int i = 0; i < length; i++)
            {
                fileVirtualAddress = new FileAddress(
                    br.ReadBigInt32(),
                    br.ReadBigInt32());
                filePhysicalAddress = new FileAddress(
                    br.ReadBigInt32(),
                    br.ReadBigInt32());

                if (fileVirtualAddress.Start == 0
                    && fileVirtualAddress.End == 0)
                    continue;

                fileRecord = new FileRecord(fileVirtualAddress, filePhysicalAddress, i);
                Table.Add(fileRecord.VirtualAddress.Start, fileRecord);
            }
            FileRecord addr;
            Table.TryGetValue(address, out addr);
            Address = addr;
        }

        private int GetEndAddress(BinaryReader br)
        {
            long seekback;
            int result;

            seekback = br.BaseStream.Position;

            //set position
            br.BaseStream.Position += 0x24; //3rd record

            result = br.ReadBigInt32();

            br.BaseStream.Position = seekback;
            return result;
        }

        public IEnumerable<FileRecord> GetUncompressedFiles()
        {
            return ((IEnumerable<FileRecord>)Table).Where(x => x.IsCompressed == false);
        }
    }
}
