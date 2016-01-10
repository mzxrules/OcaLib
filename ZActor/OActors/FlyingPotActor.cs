namespace mzxrules.ZActor.OActors
{
    class FlyingPotActor : ActorRecord_Wrapper
    {
        CollectableFlag flag;
        public FlyingPotActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Flying Pot";
        }
    }
}
