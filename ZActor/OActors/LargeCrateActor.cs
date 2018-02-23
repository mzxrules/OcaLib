namespace mzxrules.ZActor.OActors
{
    class LargeCrateActor : ActorRecord_Wrapper
    {
        public LargeCrateActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Large Crate";
        }
    }
}
