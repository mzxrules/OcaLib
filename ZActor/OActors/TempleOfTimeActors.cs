using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mzxrules.ZActor.OActors
{
    class TempleOfTimeActors : ActorRecord
    {
        public TempleOfTimeActors(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            switch (Variable)
            {
                case 0x000D: return "Door of Time";
                case 0x000F: return "Song Warp Effect";
                case 0x0010: return "Warp in Effect";
                default: return base.GetActorName();
            }
        }
    }
}
