namespace mzxrules.ZActor.OActors
{
    class BombflowerActor:ActorRecord_Wrapper
    {
        public BombflowerActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Bombflower";
        }
    }
}
