using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class BreakablePotActor : ActorRecord
    {
        CollectableFlag flags;
        byte drop;
        public BreakablePotActor(byte[] record, params int[] p)
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
