using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class GroundedSalesScrubActor:ActorRecord_Wrapper
    {

        public GroundedSalesScrubActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Ground Deku Scrub";
        }
        protected override string GetVariable()
        {
            switch (Variable)
            {
                case 0x0000: return "Deku Nuts";
                case 0x0001: return "Deku Sticks";
                case 0x0002: return "Piece of Heart";
                case 0x0003: return "Deku Seeds";
                case 0x0004: return "Deku Shield";
                case 0x0005: return "Bombs";
                case 0x0006: return "Deku Seeds";
                case 0x0007: return "Red Potion";
                case 0x0008: return "Green Potion";
                case 0x0009: return "Deku Stick Upgrade";
                case 0x000A: return "Deku Nut Upgrade";
                default: return Variable.ToString("X4");
            }
        }
    }
}
