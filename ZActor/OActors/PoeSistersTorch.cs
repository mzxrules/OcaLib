using mzxrules.Helper;
using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class PoeSistersTorch : ActorRecord_Wrapper
    {
        SwitchFlag flag;
        byte color;
        public PoeSistersTorch(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            color   = Shift.AsByte(Variable, 0xFF00);
            flag    = Shift.AsByte(Variable, 0x003F); 
        }
        protected override string GetActorName()
        {
            return "Poe Sisters Golden Torch";
        }
        protected override string GetVariable()
        {
            string colorStr;
            switch (color)
            {
                case 00: colorStr = "Purple (Meg)"; break;
                case 01: colorStr = "Red (Joelle)"; break;
                case 02: colorStr = "Blue (Beth)"; break;
                case 03: colorStr = "Green (Amy)"; break;
                default: colorStr = "???"; break;
            }
            return string.Format("Color: {0}, {1}",
                colorStr,
                flag);
        }
    }
}
