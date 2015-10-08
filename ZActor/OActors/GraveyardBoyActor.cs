using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class GraveyardBoyActor:ActorRecord_Wrapper
    {
        byte path;
        public GraveyardBoyActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            path = (byte)Variable;
        }
        protected override string GetActorName()
        {
            return "Graveyard Boy";
        }
        protected override string GetVariable()
        {
            return "Path " + path.ToString() ;
        }
    }
}
