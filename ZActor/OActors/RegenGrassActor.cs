using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mzxrules.ZActor.OActors
{
    class RegenGrassActor : ActorRecord
    {
        public RegenGrassActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Regen Grass";
        }
    }
}
