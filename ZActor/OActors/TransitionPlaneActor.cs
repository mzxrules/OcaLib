using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZActor.OActors
{
    class TransitionPlaneActor : TransitionActor
    {
        public TransitionPlaneActor(byte[] record)
            : base(record)
        {

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
    }
}
