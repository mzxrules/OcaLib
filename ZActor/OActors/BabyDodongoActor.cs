using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class BabyDodongoActor:ActorRecord_Wrapper
    {
        public BabyDodongoActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Baby Dodongo";
        }
    }
}
