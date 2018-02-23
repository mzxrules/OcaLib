using mzxrules.Helper;
using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class SpiritTempleChainPlatformActor : ActorRecord_Wrapper
    {
        SwitchFlag flags;
        public SpiritTempleChainPlatformActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flags = Shift.AsByte(Variable, 0x3F);
        }
        protected override string GetActorName()
        {
            return "Chain Platform (Spirit Temple)";
        }
        protected override string GetVariable()
        {
            return flags.ToString();
        }
    }
}
