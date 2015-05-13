using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class FlameCircleActor:ActorRecord
    {
        byte type;
        byte camera;
        SwitchFlag flags;
        public FlameCircleActor(byte[] record, params int[] p)
            : base(record, p)
        {
            type = (byte)(Variable >> 12);
            camera = (byte)((Variable & 0xF00) >> 8);
            flags = (byte)(Variable & 0x3F);
        }
        protected override string GetActorName()
        {
            return "Flame Circle";
        }
        protected override string GetVariable()
        {
            string typeStr;
            switch (type)
            {
                case 0x0: typeStr = "Large, doesn't shut off"; break;
                case 0x1: typeStr = "Small, shuts off for long time with ticking sound"; break;
                case 0x2: typeStr = "Small, shuts off for long time"; break;
                case 0x3: typeStr = "Large, shuts off for a short time with ticking sound"; break;
                case 0x4: typeStr = "Disabled, small, turns on for short time"; break;
                case 0x5: typeStr = "Disabled, large, turns on permanently"; break;
                case 0x6: typeStr = "Large, turns off permanently"; break;
                default: typeStr = "Nothing"; break;
            }
            return typeStr + ", camera " + camera.ToString("X2") + ", bound to " + flags.ToString();
        }
    }
}
