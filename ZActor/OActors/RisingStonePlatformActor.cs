﻿namespace mzxrules.ZActor.OActors
{
    class RisingStonePlatformActor : ActorRecord_Wrapper
    {
        public RisingStonePlatformActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Rising Stone Actor";
        }
    }
}
