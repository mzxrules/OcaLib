using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class KaeporaGaeboraActor : ActorRecord_Wrapper
    {
        SwitchFlag flag;
        byte type;
        public KaeporaGaeboraActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag    = Pack.AsByte(Variable, 0x003F);
            type    = Pack.AsByte(Variable, 0x0FC0);
        }
        protected override string GetActorName()
        {
            return "Kaepora Gaebora";
        }
        protected override string GetVariable()
        {
            return string.Format("Type {0}, {1}",
                type,
                flag);
        }
    }
}
