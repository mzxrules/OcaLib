namespace mzxrules.ZActor.OActors
{
    class MegaDodongoActor: ActorRecord_Wrapper
    {
        public MegaDodongoActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Dodongo's Cavern 'Mega Dodongo'";
        }
    }
}
