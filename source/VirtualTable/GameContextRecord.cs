using mzxrules.Helper;
using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class GameContextRecord : OverlayTableRecord
    {
        public const int LENGTH = 0x30;
        /* 0x00 */ uint unknown1;
        /* 0x04 */ //int VRomStart; //if applicable
        /* 0x08 */ //int VRomEnd;   //if applicable
        /* 0x0C */ //int VRamStart; //if applicable
        /* 0x10 */ //int VRamEnd;   //if applicable
        /* 0x14 */ uint unknown2;
        /* 0x18 */ N64Ptr VRamUnknown1; //Possibly execution point?
        /* 0x1C */ N64Ptr VRamUnknown2; //Possibly execution point?

        /* 0x20-0x2C */ //unknown

        /* 0x2C */ int AllocateSize; //Think it's size of initialized instance of the overlay

        static string[] Files = new string[]
        {
            "N/A",
            "ovl_select",
            "ovl_title",
            "N/A",
            "ovl_opening",
            "ovl_file_choose"
        };
        public GameContextRecord()
        { }

        public GameContextRecord (int index, BinaryReader br)
        {
            Initialize(index, br);
        }

        public GameContextRecord(int index, byte[] data)
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(data)))
                Initialize(index, br);
        }

        private void Initialize(int index, BinaryReader br)
        {
            unknown1 = br.ReadBigUInt32();
            VRom = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());
            VRam = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());
            unknown2 = br.ReadBigUInt32();
            VRamUnknown1 = br.ReadBigUInt32();
            VRamUnknown2 = br.ReadBigUInt32();
            br.BaseStream.Position += 0xC;
            AllocateSize = br.ReadBigInt32();
        }
    }
}
