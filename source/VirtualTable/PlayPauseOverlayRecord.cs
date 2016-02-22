using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class PlayPauseOverlayRecord : OverlayTableRecord
    {
        public const int LENGTH = 0x1C;

        public FileAddress RamAddress { get; protected set; }
        int Item;
        uint RamFileName;

        public PlayPauseOverlayRecord(int index, BinaryReader br)
        {
            uint ramFileStart;
            Item = index;

            ramFileStart = br.ReadBigUInt32();
            VRom = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());
            VRam = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());
            br.ReadBigUInt32();
            RamAddress = (ramFileStart == 0) ? new FileAddress() : new FileAddress(ramFileStart, ramFileStart + VRam.Size);
            RamFileName = br.ReadBigUInt32();
        }

        public PlayPauseOverlayRecord(int index, byte[] data)
        {
            uint ramFileStart;
            Item = index;

            ramFileStart = BitConverter.ToUInt32(data, 0x0);

            VRom = new FileAddress(
                BitConverter.ToUInt32(data, 0x04),
                BitConverter.ToUInt32(data, 0x08));
            VRam = new FileAddress(
                BitConverter.ToUInt32(data, 0x0C),
                BitConverter.ToUInt32(data, 0x10));
            RamAddress = new FileAddress(ramFileStart, ramFileStart + VRam.Size);

            RamFileName = BitConverter.ToUInt32(data, 0x18);

        }
    }
}
