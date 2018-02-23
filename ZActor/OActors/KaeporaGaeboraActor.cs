using mzxrules.Helper;
using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class KaeporaGaeboraActor : ActorRecord_Wrapper
    {
        SwitchFlag flag;
        byte type;
        public KaeporaGaeboraActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag    = Shift.AsByte(Variable, 0x003F);
            type    = Shift.AsByte(Variable, 0x0FC0);
        }
        protected override string GetActorName()
        {
            return "Kaepora Gaebora";
        }
        protected override string GetVariable()
        {
            return string.Format("Type {0}, {1}",
                type,
                flag);
        }
    }
}
