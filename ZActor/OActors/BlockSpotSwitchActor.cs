using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class BlockSpotSwitchActor:ActorRecord
    {
        SwitchFlag flag;
        public BlockSpotSwitchActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = (byte)(Variable);
        }
        protected override string GetActorName()
        {
            return "Block Spot Switch";
        }
        protected override string GetVariable()
        {
            return flag.ToString();
        }
    }
}
