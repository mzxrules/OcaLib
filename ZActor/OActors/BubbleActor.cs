using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class BubbleActor:ActorRecord
    {
        public BubbleActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            //TODO: Alternate Type
            return "Bubble (flying skull)";
        }
    }
}
