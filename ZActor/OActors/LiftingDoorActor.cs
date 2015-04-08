using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZActor.OActors
{
    class LiftingDoorActor : TransitionActor
    {
        byte type;
        SwitchFlag flags;
        public LiftingDoorActor(byte[] record)
            : base(record)
        {

            flags = (byte)(Variable & 0x3F);
            type = (byte)((Variable & 0xFC0) >> 6);
        }
        protected override string GetActorName()
        {
            string doorType;
            switch (type)
            {
                case 0x00:/*00*/ doorType = "Lifting Door"; break;
                case 0x01:/*04*/ doorType = "Front Clear Door"; break;
                case 0x02:/*08*/ doorType = "Front Switch Door"; break;
                case 0x03:/*0C*/ doorType = "Back Permlock Door"; break;
                case 0x05:/*14*/ doorType = "Boss Door"; break;
                case 0x07:/*1C*/ doorType = "Front Switch, Back Clear Door"; break;
                case 0x0B:/*2C*/ doorType = "Locked Door"; break;
                default: doorType = "Unknown Door"; break;
            }
            return doorType;
        }

        protected override string GetVariable()
        {
            return flags.ToString();
        }

        //public override string Print()
        //{
            
        //    return String.Format("{3}, {1} Door: {0}, {2}",
        //        flags.Print(),
        //        doorType,
        //        PrintWithoutActor(),
        //        PrintTransition());
        //}
    }
}
