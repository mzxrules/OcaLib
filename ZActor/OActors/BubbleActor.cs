namespace mzxrules.ZActor.OActors
{
    class BubbleActor:ActorRecord_Wrapper
    {
        public BubbleActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            //TODO: Alternate Type
            return "Bubble (flying skull)";
        }
    }
}
