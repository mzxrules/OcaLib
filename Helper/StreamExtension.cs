using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.Helper
{
    public static class StreamExtension
    {
        static byte[] Padding = new byte[0x10];
        public static bool PadToLength(this Stream sw, long length)
        {
            int offset;
            int count;
            //if the pad to length isn't greater than the stream position, 
            //don't pad the stream
            if (!sw.CanWrite)
                return false;
            if (!(length > sw.Position))
                return true;

            offset = (int)(sw.Position % Padding.Length);
            if (offset != 0)
            {
                count = (int)(Padding.Length - offset);
                sw.Write(Padding, offset, count);
            }
            count = (int)((length - sw.Position) / Padding.Length);
            for (int i = 0; i < count; i++)
            {
                sw.Write(Padding, 0, Padding.Length);
            }
            return true;
        }
        public static bool PadToNextLine(this Stream sw)
        {
            int offset;
            if (!sw.CanWrite)
                return false;

            offset = (int)(0x10 - (sw.Length % 0x10)) & 0x0F;
            return sw.Pad(offset);
        }
        public static bool Pad(this Stream sw, int i)
        {
            if (!sw.CanWrite)
                return false;
            for (; i > 0; i--)
                sw.WriteByte(0);
            return true;
        }
    }
}
