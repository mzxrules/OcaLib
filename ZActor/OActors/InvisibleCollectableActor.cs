using mzxrules.Helper;
using System;

namespace mzxrules.ZActor.OActors
{
    public class InvisibleCollectableActor : ActorRecord_Wrapper
    {
        byte a1;
        byte t8;
        byte t0;

        public InvisibleCollectableActor(short[] b, params int[] p)
            : base(b)
        {
            objectDependencies = p;
            a1 = Shift.AsByte(Variable, 0x003F);
            t8 = Shift.AsByte(Variable, (0x1F << 11));
            t0 = Shift.AsByte(Variable, (0x1F << 6));
        }
        protected override string GetActorName()
        {
            return "Invisible Collectible";
        }
        protected override string GetVariable()
        {
            return String.Format("{0:X2} {1:X2} {2:X2}", t8, t0, a1);
        }
    }
}
