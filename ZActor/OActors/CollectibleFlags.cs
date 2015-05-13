using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            value = (byte)((variable & mask) >> Pack.GetShift(mask));
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
