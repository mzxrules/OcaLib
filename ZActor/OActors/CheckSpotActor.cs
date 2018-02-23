﻿using mzxrules.Helper;
using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class CheckSpotActor:ActorRecord_Wrapper
    {
        byte type;
        byte dialog;
        SwitchFlag flag;
        public CheckSpotActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = Shift.AsByte(Variable, 0xC000);
            dialog = Shift.AsByte(Variable, 0x3FC0);
            flag = Shift.AsByte(Variable, 0x003F);
        }
        protected override string GetActorName()
        {
            return "Check Spot";
        }
        protected override string GetVariable()
        {
            string typeString;
            string textDialog;
            switch (type)
            {
                case 0: typeString = "Checkable"; break;
                case 1: typeString = "Instant"; break;
                case 2: typeString = "Disappears on Switch"; break;
                default: typeString = "Z-target, no text"; break;
            }

            textDialog = ActorDialog.Get(0x0185, dialog);
            return string.Format("{0}, dialog {1}, {2}",
                typeString, 
                (textDialog.Length < 40)? textDialog: textDialog.Substring(0, 40), 
                flag.ToString());
        }
    }
}
