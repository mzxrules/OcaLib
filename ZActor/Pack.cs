using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mzxrules.ZActor
{
    public static class Pack
    {
        public static bool AsBool(UInt16 value, UInt16 mask)
        {
            return ((value & mask) >> GetShift(mask)) > 0;
        }
        public static byte AsByte(UInt16 value, UInt16 mask)
        {
            return (byte)((value & mask) >> GetShift(mask));
        }
        public static sbyte AsSByte(UInt16 value, UInt16 mask)
        {
            return (sbyte)((value & mask) >> GetShift(mask));
        }
        public static UInt16 AsUInt16(UInt16 value, UInt16 mask)
        {
            return (UInt16)((value & mask) >> GetShift(mask));
        }
        public static int GetShift(UInt16 mask)
        {
            int shift;

            //a mask of 0 isn't valid, but could be set in the xml file by mistake.
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
