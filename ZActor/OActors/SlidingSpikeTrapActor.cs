using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class SlidingSpikeTrapActor:ActorRecord
    {
        public SlidingSpikeTrapActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Sliding Metal Spike Trap";
        }
    }
}
