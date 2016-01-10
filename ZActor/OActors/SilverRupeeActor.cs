namespace mzxrules.ZActor.OActors
{
    class SilverRupeeActor:ActorRecord_Wrapper
    {
        ushort type;
        SwitchFlag flags;
        ushort count;
        public SilverRupeeActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (ushort)((Variable & 0xF000)>>12);
            count = (ushort)((Variable & 0xFC0) >> 6);
            flags = (byte)(Variable & 0x3F);
        }
        protected override string GetActorName()
        {
            return "Silver Rupee";
        }
        protected override string GetVariable()
        {
            string typeName;

            switch (type)
            {
                //case 0x0140: typeName = "End"; break;
                //case 0x1FC0: typeName = "Chain"; break;
                case 0x0: typeName = string.Format("Collect {0} Rupees", count); break;
                case 0x1: typeName = "Rupee"; break;
                default: typeName = "???"; break;
            }
            return string.Format("Type: {0}, {1}",
                typeName,
                flags.ToString());
        }
    }
}
