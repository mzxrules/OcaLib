using System;
using mzxrules.Helper;

namespace mzxrules.ZActor.OActors
{
    public class CollectableFlag
    {
        byte value;
        CollectableFlag(byte b) 
        {
            value = b;
        }
        public CollectableFlag(UInt16 variable, UInt16 mask)
        {
            value = Shift.AsByte(variable, mask); //(byte)((variable & mask) >> Shift.GetShift(mask));
        }
        public static implicit operator byte(CollectableFlag s)
        {
            return s.value;
        }
        public static implicit operator CollectableFlag(byte b)
        {
            return new CollectableFlag(b);
        }
        public override string ToString()
        {
            if (value < 0x20)
            {
                return String.Format("Perm: {0:X2}",value);
            }
            else 
            {
                return String.Format("Temp: {0:X2}", (value - 0x20));
            }
        }
    }
}
