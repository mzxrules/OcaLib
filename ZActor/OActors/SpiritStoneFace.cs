namespace mzxrules.ZActor.OActors
{
    class SpiritStoneFaceActor : ActorRecord_Wrapper
    {
        SwitchFlag flag;
        public SpiritStoneFaceActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = (byte)Variable;
        }
        protected override string GetActorName()
        {
            return "Spirit Statue Stone Face";
        }
        protected override string GetVariable()
        {
            return flag.ToString();
        }
    }
}
