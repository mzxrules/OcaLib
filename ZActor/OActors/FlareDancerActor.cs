using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class FlareDancerActor: ActorRecord
    {
        public FlareDancerActor(byte[] record):base(record)
        {
        }
        protected override string GetActorName()
        {
            return "Flare Dancer";
        }
    }
}
