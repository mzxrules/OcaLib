namespace mzxrules.ZActor.OActors
{
    class BabyDodongoActor:ActorRecord_Wrapper
    {
        public BabyDodongoActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Baby Dodongo";
        }
    }
}
