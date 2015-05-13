using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class IronKnuckleRoomActor : ActorRecord
    {
        byte type;
        public IronKnuckleRoomActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (byte)(Variable % 2);
        }
        protected override string GetActorName()
        {
            if (type == 0)
                return "Brick Pillar";
            else
                return "Brick Throne";
        }
    }
}
