namespace mzxrules.ZActor.OActors
{
    class ArmosStatueActor : ActorRecord_Wrapper
    {
        // TODO: Actor Variables
        public ArmosStatueActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            if (Variable == 0)
                return "Pushable Armos";
            else
                return "Armos Monster";
        }
    }
}
