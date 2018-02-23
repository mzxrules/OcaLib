﻿namespace mzxrules.ZActor.OActors
{
    class IronKnuckleRoomActor : ActorRecord_Wrapper
    {
        byte type;
        public IronKnuckleRoomActor(short[] record, params int[] p)
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
