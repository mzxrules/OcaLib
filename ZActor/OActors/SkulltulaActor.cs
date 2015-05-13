using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class SkulltulaActor:ActorRecord
    {
        byte type;
        public SkulltulaActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (byte)Variable;
        }
        protected override string GetActorName()
        {
            switch (type)
            {
                case 0x1: return "Big Skulltula";
                case 0x2: return "Invisible Skulltula";
                default: return "Skulltula";
            }
        }
    }
}
