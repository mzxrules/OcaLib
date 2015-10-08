using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.Helper
{
    public static class Shift
    {
        const ulong LEFT = 0x8000000000000000;
        public static bool AsBool(UInt16 value, UInt16 mask)
        {
            return ((value & mask) >> GetRight(mask)) > 0;
        }
        public static byte AsByte(UInt16 value, UInt16 mask)
        {
            return (byte)((value & mask) >> GetRight(mask));
        }
        public static sbyte AsSByte(UInt16 value, UInt16 mask)
        {
            return (sbyte)((value & mask) >> GetRight(mask));
        }
        public static UInt16 AsUInt16(UInt16 value, UInt16 mask)
        {
            return (UInt16)((value & mask) >> GetRight(mask));
        }
        public static int GetLeft(UInt16 mask)
        {
            ulong v = mask;
            v <<= 48;
            return GetLeft(v);
        }
        public static int GetLeft(UInt32 mask)
        {
            ulong v = mask;
            v <<= 32;
            return GetLeft(v);
        }
        public static int GetLeft(ulong mask)
        {
            int shift;

            if (mask == 0)
                return 0;

            for (shift = 0; (mask & LEFT) == 0; mask <<= 1)
                shift++;

            return shift;
        }
        public static int GetRight(ulong mask)
        {
            int shift;

            //a mask of 0 isn't valid, but could come up by accident
            if (mask == 0)
                return 0;

            //Check the right bit
            //If the right bit is 0 shift the mask over one and increment the shift count
            //If the right bit is 1, the right side of the mask is found, we know how much to shift by
            for (shift = 0; (mask & 1) == 0; mask >>= 1)
            {
                shift++;
            }

            return shift;
        }
    }
}
