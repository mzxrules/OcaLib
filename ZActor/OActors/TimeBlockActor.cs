namespace mzxrules.ZActor.OActors
{
    class TimeBlockActor:ActorRecord_Wrapper, ISwitchFlag
    {
        public SwitchFlag Flag { get { return flag; } set { flag = value; } }
        SwitchFlag flag;

        public TimeBlockActor(byte[] record, params int[] p)
            : base(record, p)
        {
            flag = (byte)(Variable & 0x3F);
        }
        protected override string GetActorName()
        {
            return "Time Block";
        }

        protected override string GetVariable()
        {
            return flag.ToString();
        }

        public SwitchFlagAttributes GetFlagAttributes()
        {
            return SwitchFlagAttributes.WriteSwitch;
        }
    }
}
