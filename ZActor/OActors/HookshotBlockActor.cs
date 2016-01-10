namespace mzxrules.ZActor.OActors
{
    class HookshotBlockActor:ActorRecord_Wrapper
    {
        public HookshotBlockActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Hookshot Block";
        }
        protected override string GetVariable()
        {
            //FFC2 hookshot block
            return base.GetVariable();
        }
    }
}
