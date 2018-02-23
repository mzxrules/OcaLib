namespace mzxrules.ZActor.OActors
{
    class GraveyardBoyActor:ActorRecord_Wrapper
    {
        byte path;
        public GraveyardBoyActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            path = (byte)Variable;
        }
        protected override string GetActorName()
        {
            return "Graveyard Boy";
        }
        protected override string GetVariable()
        {
            return "Path " + path.ToString() ;
        }
    }
}
