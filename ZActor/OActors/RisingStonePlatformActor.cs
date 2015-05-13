using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class RisingStonePlatformActor : ActorRecord
    {
        public RisingStonePlatformActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Rising Stone Actor";
        }
    }
}
