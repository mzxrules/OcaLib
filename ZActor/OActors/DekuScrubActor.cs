namespace mzxrules.ZActor.OActors
{
    class DekuScrubActor:ActorRecord_Wrapper
    {
        public DekuScrubActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Deku Scrub";
        }
    }
}
