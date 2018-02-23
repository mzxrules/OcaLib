namespace mzxrules.ZActor.OActors
{
    class FlareDancerActor: ActorRecord_Wrapper
    {
        public FlareDancerActor(short[] record):base(record)
        {
        }
        protected override string GetActorName()
        {
            return "Flare Dancer";
        }
    }
}
