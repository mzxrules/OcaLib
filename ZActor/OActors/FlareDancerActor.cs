namespace mzxrules.ZActor.OActors
{
    class FlareDancerActor: ActorRecord_Wrapper
    {
        public FlareDancerActor(byte[] record):base(record)
        {
        }
        protected override string GetActorName()
        {
            return "Flare Dancer";
        }
    }
}
