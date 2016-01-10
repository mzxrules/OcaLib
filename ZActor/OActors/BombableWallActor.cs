namespace mzxrules.ZActor.OActors
{
    class BombableWallActor: ActorRecord_Wrapper, ISwitchFlag
    {
        bool dodongoCutscene;
        public SwitchFlag Flag { get { return flag; } set { flag = value; } }
        SwitchFlag flag;
        public BombableWallActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = (byte)(Variable & 0x3F);
            dodongoCutscene = ((Variable & 0xA000) == 0);
        }
        protected override string GetActorName()
        {
            return "Bombable Wall";
        }
        protected override string GetVariable()
        {
            return string.Format("{0}{1}",
                (dodongoCutscene) ? "Plays Dodongo's Cavern Cutscene " : "",
                flag.ToString());
        }

        public SwitchFlagAttributes GetFlagAttributes()
        {
            return SwitchFlagAttributes.WriteSwitch;
        }
    }
}
