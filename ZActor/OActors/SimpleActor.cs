using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class SimpleActor:ActorRecord_Wrapper
    {
        private string name;

        public SimpleActor(byte[] record, string p1, params int[] p2):base (record, p2)
        {
            this.name = p1;
        }
        protected override string GetActorName()
        {
            return name;
        }
    }
}
