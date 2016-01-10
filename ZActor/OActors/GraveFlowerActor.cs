namespace mzxrules.ZActor.OActors
{
    class GraveFlowerActor: ActorRecord_Wrapper
    {
        public GraveFlowerActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Grave Flower";
        }
    }
}
