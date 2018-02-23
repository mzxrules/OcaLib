﻿namespace mzxrules.ZActor.OActors
{
    class RedeadActor: ActorRecord_Wrapper
    {
        bool gibdo;
        byte type;
        public RedeadActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (byte)Variable;
            gibdo = ((type & 0x80)> 0);
        }
        protected override string GetActorName()
        {
            switch (gibdo)
            {
                case true: return "Gibdo";
                default: return "Redead";
            }
        }
        protected override string GetVariable()
        {
            if (gibdo)
            {
                switch (type)
                {
                    case 0xFE: return "Standing";
                    default: return "Unknown";
                }
            }
            else
            {
                switch (type)
                {
                    case 2: return "Crouching";
                    case 3: return "Invisible";
                    default: return "Standing";
                }
            }
        }
    }
}
