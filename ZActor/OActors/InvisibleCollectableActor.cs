using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    public class InvisibleCollectableActor : ActorRecord_Wrapper
    {
        byte a1;
        byte t8;
        byte t0;

        public InvisibleCollectableActor(byte[] b, params int[] p)
            : base(b)
        {
            objectDependencies = p;
            a1 = Pack.AsByte(Variable, 0x003F);
            t8 = Pack.AsByte(Variable, (0x1F << 11));
            t0 = Pack.AsByte(Variable, (0x1F << 6));
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
