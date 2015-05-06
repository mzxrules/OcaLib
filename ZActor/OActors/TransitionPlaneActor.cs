using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZActor.OActors
{
    class TransitionPlaneActor : TransitionActor
    {
        SwitchFlag flag;
        ushort type;
        public TransitionPlaneActor(byte[] record)
            : base(record)
        {
            flag = Pack.AsByte(Variable, 0x003F);
            type = Pack.AsUInt16(Variable, 0xFFC0);
        }
        //public override string Print()
        //{
        //    return String.Format("{0}, Transition Plane, {1}",
        //        PrintTransition(),
        //        PrintWithoutActor());
        //}
        protected override string GetActorName()
        {
            return "Transition Plane";
        }
        protected override string GetVariable()
        {
            return string.Format("{2} Type {0}: {1}", type, flag, this.PrintTransition());
        }
    }
}
