namespace mzxrules.ZActor.OActors
{
    class DekuScrubActor:ActorRecord_Wrapper
    {
        public DekuScrubActor(byte[] record, params int[] p)
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
