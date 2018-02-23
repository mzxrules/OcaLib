namespace mzxrules.ZActor.OActors
{
    class GraveFlowerActor: ActorRecord_Wrapper
    {
        public GraveFlowerActor(short[] record, params int[] p)
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
