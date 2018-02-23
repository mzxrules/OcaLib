namespace mzxrules.ZActor.OActors
{
    class RegenGrassActor : ActorRecord_Wrapper
    {
        public RegenGrassActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Regen Grass";
        }
    }
}
