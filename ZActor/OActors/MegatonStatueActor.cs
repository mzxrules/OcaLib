using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class MegatonStatueActor:ActorRecord_Wrapper
    {
        SwitchFlag flag;
        byte type;
        public MegatonStatueActor(short[] record, params int[] p)
            : base(record, p)
        {
            flag = new SwitchFlag(Variable, 0x3F00);
            type = (byte)(Variable & 0xFF);
        }
        protected override string GetActorName()
        {
            switch (type)
            {
                case 0x01: return "Megaton Statue Head";
                default: return "Megaton Statue Base";
            }
        }
        protected override string GetVariable()
        {
            return flag.ToString();
        }
    }
}
