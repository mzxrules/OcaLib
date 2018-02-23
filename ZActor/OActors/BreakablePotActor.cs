﻿using mzxrules.OcaLib.Actor;
using System;

namespace mzxrules.ZActor.OActors
{
    class BreakablePotActor : ActorRecord_Wrapper
    {
        CollectableFlag flags;
        byte drop;
        public BreakablePotActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            //rightmost bit of left byte unused?
            flags = new CollectableFlag(Variable, 0x7E00); //(byte)(Variable >> 9); 
            drop = (byte)Variable;
        }
        protected override string GetActorName()
        {
            return "Breakable Pot";
        }
        protected override string GetVariable()
        {
            return String.Format("Collectable: {0:X2} {1}", drop, flags);
        }
    }
}
