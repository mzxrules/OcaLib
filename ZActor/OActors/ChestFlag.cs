using System;

namespace mzxrules.ZActor.OActors
{
    class ChestFlag
    {
        byte value;
        ChestFlag(byte b) 
        {
            value = b;
        }

        
        public static implicit operator byte(ChestFlag s)
        {
            return s.value;
        }
        public static implicit operator ChestFlag(byte b)
        {
            return new ChestFlag(b);
        }
        public override string ToString()
        {
            if (value < 0x20)
            {
                return String.Format("chest flag: {0:X2}", value);
            }
            else
                return "invalid";
        }
    }
}
