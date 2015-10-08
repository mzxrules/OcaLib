using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class TorchActor : ActorRecord_Wrapper
    {
        Byte type;
        SwitchFlag flags;
        int torchCount = 0;
        public TorchActor(byte[] record, params int[] p)
            : base(record, p)
        {
            flags = new SwitchFlag(Variable, 0x3F);
            type = Pack.AsByte(Variable, 0xFC00);
            torchCount = Pack.AsByte(Variable, 0x03C0);
        }
        //TODO: Actor Variables
        protected override string GetActorName()
        {
            string typename;
            switch (type)
            {
                case 0x00: //0x03C0
                    typename = "Golden Torch"; break;// showflag = true; break;
                case 0x04: //0x1000
                    typename = "Timed Torch"; break;
                case 0x08: //0x2000
                    typename = "Unlit Wooden Torch"; break;
                case 0x09: //0x2400
                    typename = "Lit Wooden Torch"; break;
                default:
                    typename = "??? Torch"; break;
            }
            return typename;
        }
        protected override string GetVariable()
        {
            switch (type)
            {
                case 0x00: //0x03C0
                    return flags.ToString();
                case 0x04: //0x1000
                    return String.Format("Light: {0}, {1}", torchCount, flags.ToString());
                case 0x08: //0x2000
                    return flags.ToString();
                case 0x09:
                    return flags.ToString();
                default:
                    return base.GetVariable();
            }
        }
    }
}
