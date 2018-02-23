﻿using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class WellWaterActor:ActorRecord_Wrapper
    {
        SwitchFlag flag;
        public WellWaterActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = (byte)(Variable & 0x3F);
        }

        protected override string GetActorName()
        {
            return "Bottom of the Well Water";
        }

        protected override string GetVariable()
        {
            return flag.ToString();
        }
    }
}
