namespace mzxrules.ZActor.OActors
{
    class GossipStoneActor:ActorRecord_Wrapper
    {

        public GossipStoneActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Gossip Stone";
        }
    }
}
