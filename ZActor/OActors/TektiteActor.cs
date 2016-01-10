namespace mzxrules.ZActor.OActors
{
    class TektiteActor : ActorRecord_Wrapper
    {
        public TektiteActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            switch (Variable)
            {
                case 0xFFFE: return "Blue Tektite";
                default: return "Red Tektite";
            }
        }
    }
}
