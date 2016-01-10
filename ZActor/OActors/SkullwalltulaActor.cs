using mzxrules.Helper;

namespace mzxrules.ZActor.OActors
{
    class SkullwalltulaActor : ActorRecord_Wrapper
    {
        byte type;
        byte group;
        byte flag;
        public SkullwalltulaActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = Shift.AsByte(Variable, 0xE000);
            group = Shift.AsByte(Variable, 0x1F00);
            flag = Shift.AsByte(Variable, 0x00FF);
        }
        protected override string GetActorName()
        {
            if (type == 0)
                return "Skullwalltula";
            else
                return "Gold Skulltula";
        }
        protected override string GetVariable()
        {
            return string.Format("Type {0}, Group {1:X2}, Flag {2:X2}", type, group, flag);
        }
    }
}
