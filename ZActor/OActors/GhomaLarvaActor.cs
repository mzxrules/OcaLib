﻿namespace mzxrules.ZActor.OActors
{
    class GhomaLarvaActor:ActorRecord_Wrapper
    {
        public GhomaLarvaActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Ghoma Larva";
        }
    }
}
