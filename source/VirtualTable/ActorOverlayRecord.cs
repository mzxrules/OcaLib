using mzxrules.Helper;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib
{
    public class ActorOverlayRecord : OverlayTableRecord
    {
        #region Structure
        /* 00: [Vrom] File Start, End
         * 08: [Vram] File Start, End
         * 10: [Ram]  File location
         * 14: [Vram] Actor Info?
         * 18: [Ram]  File name location
         * 1C: [?]    Unknown
         */

        //Main data structure
        //public FileAddress VRom;
        //public FileAddress VRam;
        //uint ramFileStart;
        public N64Ptr VRamActorInfo;
        public N64Ptr RamFileName;
        public uint Unknown;
        #endregion

        #region Extended Fields

        public const int LENGTH = 0x20;

        public FileAddress Ram { get; protected set; }

        public int Actor { get; protected set; }

        #endregion

        protected ActorOverlayRecord() { }

        public ActorOverlayRecord(int index, BinaryReader br)
        {
            Actor = index;
            VRom = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());
            VRam = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());

            /* ramFileStart = */ br.ReadBigUInt32();
            VRamActorInfo = br.ReadBigUInt32();
            RamFileName = br.ReadBigUInt32();
            Unknown = br.ReadBigUInt32();
        }

        /// <summary>
        /// Initializes the actor overlay record. Assumes data is stored in Big Endian form
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public ActorOverlayRecord(int index, byte[] data)
        {
            Create(index, GetData_Rom(data, LENGTH));
        }
        
        private void Create(int index, List<uint> data)
        {
            N64Ptr ramFileStart;
            Actor = index;
            VRom = new FileAddress(data[0], data[1]);
            VRam = new FileAddress(data[2], data[3]);

            ramFileStart = data[4];
            VRamActorInfo = data[5];
            RamFileName = data[6];
            Unknown = data[7];

            Ram = new FileAddress(ramFileStart, ramFileStart + VRam.Size);
        }
    }
}