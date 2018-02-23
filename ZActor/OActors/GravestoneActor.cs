﻿namespace mzxrules.ZActor.OActors
{
    class GravestoneActor: ActorRecord_Wrapper
    {
        bool type;
        public GravestoneActor(short[] record, params int[] p)
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
