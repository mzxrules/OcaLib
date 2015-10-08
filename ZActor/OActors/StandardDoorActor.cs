using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class StandardDoorActor: TransitionActor
    {
        SwitchFlag flag;
        bool locked;
        public StandardDoorActor(byte[] record)
            : base(record)
        {
            flag = (byte)(Variable & 0x3F);
            locked = ((Variable & 0x80) > 0);
        }
        protected override string GetActorName()
        {
            return (locked) ? "Locked Door" : "Door";
        }
        protected override string GetVariable()
        {
            return flag.ToString();
        }
        //public override string Print()
        //{
        //    return string.Format("{3}, {0}Door: {1}, {2}",
        //        ,
        //        flag.Print(),
        //        PrintWithoutActor(),
        //        PrintTransition());
        //}
    }
}
