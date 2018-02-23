namespace mzxrules.ZActor.OActors
{
    class DodongoActor : ActorRecord_Wrapper
    {
        public DodongoActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Dodongo";
        }
    }
}
