using mzxrules.Helper;
using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class WolfosActor : ActorRecord_Wrapper
    {
        SwitchFlag flag;
        public WolfosActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = Shift.AsByte(Variable, 0x3F00);
        }
        protected override string GetActorName()
        {
            return "Wolfos";
        }
        protected override string GetVariable()
        {
            return flag.ToString();
        }
    }
}
