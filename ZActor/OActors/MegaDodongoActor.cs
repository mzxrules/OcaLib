using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class MegaDodongoActor: ActorRecord
    {
        public MegaDodongoActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Dodongo's Cavern 'Mega Dodongo'";
        }
    }
}
