using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class GrottoActor : ActorRecord
    {
        bool type;
        byte appearOn;
        byte contents;
        byte flag;
        ushort destination;

        public GrottoActor(byte[] record, params int[] p)
            : base(record)
        {
            //Mask FCFF?
            type = Pack.AsBool(Variable, 0x1000);
            appearOn = Pack.AsByte(Variable, 0x0300);
            contents = Pack.AsByte(Variable, 0x00E0);
            flag = Pack.AsByte(Variable, 0x001F);
            destination = this.rotation.z;
        }
        protected override string GetActorName()
        {
            return "Grotto";
        }
        protected override string GetVariable()
        {
            return string.Format("{3}, {0}, Chest: {1}, Flag {2:X2}",
                AppearOn(),
                Contents(),
                flag,
                Destination());
        }

        private string AppearOn()
        {
            switch (appearOn)
            {
                case 0: return "Visible";
                case 1: return "Song of Storms";
                case 2: return "Hit";
                default: return "???";
            }
        }

        private string Destination()
        {
            if (!type)
            {
                switch (destination)
                {
                    case 0x0: return "Generic";
                    case 0x1: return "Big Skulltula";
                    case 0x2: return "Heart Piece Scrub";
                    case 0x3: return "Two Redeads";
                    case 0x4: return "Three Deku Salescrubs";
                    case 0x5: return "Webbed";
                    case 0x6: return "Octorok";
                    case 0x7: return "Two Deku Salescrubs - Deku Nut Upgrade";
                    case 0x8: return "Two Wolfos";
                    case 0x9: return "Bombable wall";
                    case 0xA: return "Two Deku Salescrubs - Green Pot";
                    case 0xB: return "Tektite";
                    case 0xC: return "Forest Stage";
                    case 0xD: return "Cow";
                    default: return "???";
                }
            }
            else if (destination == 0)
                return "Fairy Fountain";
            return "???";
        }

        private string Contents()
        {
            switch (contents)
            {
                case 0: return "Blue Rupee";
                case 1: return "Red Rupee";
                case 2: return "Yellow Rupee";
                case 3: return "Bombs (20)";
                default:
                    return (!type && (destination == 0)) ? "Bombs (1)" : "None";
            }
        }
    }
}
