namespace mzxrules.ZActor.OActors
{
    class DodongoActor : ActorRecord_Wrapper
    {
        public DodongoActor(byte[] record, params int[] p)
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
