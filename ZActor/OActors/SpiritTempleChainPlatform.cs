using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class SpiritTempleChainPlatformActor : ActorRecord
    {
        SwitchFlag flags;
        public SpiritTempleChainPlatformActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flags = Pack.AsByte(Variable, 0x3F);
        }
        protected override string GetActorName()
        {
            return "Chain Platform (Spirit Temple)";
        }
        protected override string GetVariable()
        {
            return flags.ToString();
        }
    }
}
