namespace mzxrules.ZActor.OActors
{
    class DecorativeFlameActor : ActorRecord_Wrapper
    {
        public DecorativeFlameActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Decorative Flame";
        }
    }
}
