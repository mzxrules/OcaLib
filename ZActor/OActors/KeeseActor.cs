using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class KeeseActor:ActorRecord_Wrapper
    {
        byte type;
        public KeeseActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (byte)Variable;
        }
        protected override string GetActorName()
        {
            switch (type)
            {
                case 0: return "Fire Keese";
                case 1: return "Fire Keese";
                case 2: return "Aggressive Keese";
                case 3: return "Roosting Keese";
                case 4: return "Ice Keese";
                default: return "? Keese";
            }
        }
    }
}
