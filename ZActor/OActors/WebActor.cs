namespace mzxrules.ZActor.OActors
{
    class WebActor : ActorRecord_Wrapper
    {
        SwitchFlag setWhenBrokenFlag;
        SwitchFlag burnFlag;
        byte type;
        public WebActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            setWhenBrokenFlag = (byte)(Variable & 0x3F);
            burnFlag = new SwitchFlag(Variable, 0x0FC0);
            type = (byte)((Variable & 0xF000)>>12);
        }
        protected override string GetActorName()
        {
            return "Web";
        }
        protected override string GetVariable()
        {
            string typeStr;
            switch (type)
            {
                case 0: typeStr = "Springy"; break;
                case 1: typeStr = "Wall"; break;
                default: typeStr = "???"; break;
            }
            return string.Format("{0}, {1}, Burn on {2}", typeStr, setWhenBrokenFlag, burnFlag);
        }
    }
}
