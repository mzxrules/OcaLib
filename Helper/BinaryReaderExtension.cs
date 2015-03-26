using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mzxrules.OcaLib.Helper
{
    public static class BinaryReaderExtension
    {

        public static Int16 ReadBigInt16(this BinaryReader b)
        {
            return Endian.ConvertShort(b.ReadInt16());
        }
        public static Int32 ReadBigInt32(this BinaryReader b)
        {
            return Endian.ConvertInt32(b.ReadInt32());
        }

        public static UInt16 ReadBigUInt16(this BinaryReader b)
        {
            return Endian.ConvertUShort(b.ReadUInt16());
        }
        public static UInt32 ReadBigUInt32(this BinaryReader b)
        {
            return Endian.ConvertUInt32(b.ReadUInt32());
        }

    }
}
