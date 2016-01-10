namespace mzxrules.ZActor.OActors
{
    class GreenNaviSpotActor:ActorRecord_Wrapper
    {
        public GreenNaviSpotActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Green Navi Spot";
        }
    }
}
