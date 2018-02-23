﻿using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class StoneStairsActor : ActorRecord_Wrapper
    {
        SwitchFlag flags;
        public StoneStairsActor(short[] record, params int[] p)
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
