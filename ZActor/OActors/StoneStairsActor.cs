using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class StoneStairsActor : ActorRecord_Wrapper
    {
        SwitchFlag flags;
        public StoneStairsActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flags = (byte)Variable;
        }
        protected override string GetActorName()
        {
            return "Giant Stone Stairs";
        }
        protected override string GetVariable()
        {
            return flags.ToString();
        }
    }
}
