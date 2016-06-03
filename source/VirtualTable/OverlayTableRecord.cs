using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class OverlayTableRecord
    {
        public FileAddress VRom { get; set; }
        public FileAddress VRam { get; set; }


        protected static List<uint> GetData_Rom(byte[] data, int LENGTH)
        {
            List<uint> d = new List<uint>();

            for (int i = 0; i < LENGTH; i += 4)
            {
                var v = BitConverter.ToUInt32(data, i);
                d.Add(Endian.ConvertUInt32(v));
            }
            return d;
        }

        protected static List<uint> GetData_Ram(byte[] data, int LENGTH)
        {
            List<uint> d = new List<uint>();

            for (int i = 0; i < LENGTH; i += 4)
            {
                var v = BitConverter.ToUInt32(data, i);
                d.Add(v);
            }
            return d;
        }
    }
}
