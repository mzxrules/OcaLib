using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class HugeStoneElevatorActor:ActorRecord 
    {
        byte speed;
        byte height;
        byte size;
        public HugeStoneElevatorActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            height  = Pack.AsByte(Variable, 0xF000);
            speed   = Pack.AsByte(Variable, 0x0F00);
            size    = Pack.AsByte(Variable, 0x0001);
        }
        protected override string GetActorName()
        {
            return "Huge Stone Elevator";
        }
        protected override string GetVariable()
        {
            return string.Format("{2}, Height: {0}, Speed: {1:X2}",
                (height * 80),
                speed,
                (size == 0) ? "Small" : "Large");
        }
    }
}
