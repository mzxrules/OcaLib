﻿namespace mzxrules.ZActor.OActors
{
    class BeamosActor: ActorRecord_Wrapper 
    {
        //TODO: Actor variables
        public BeamosActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Beamos";
        }
    }
}
