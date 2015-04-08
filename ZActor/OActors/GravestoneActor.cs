using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class GravestoneActor: ActorRecord
    {
        bool type;
        public GravestoneActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = Variable > 0;
        }
        protected override string GetActorName()
        {
            return "Grave";
        }
        protected override string GetVariable()
        {
            if (type == true)
                return "Discovery Sound";
            else
                return base.GetVariable();
        }
    }
}
