using mzxrules.Helper;
using mzxrules.OcaLib.Actor;
using System;

namespace mzxrules.ZActor.OActors
{
    public class NaviInfospotActor : ActorRecord_Wrapper
    {
        const int TEXT_CAP = 80;
        bool naviIcon;
        SwitchFlag flag;
        byte dialog;
        public NaviInfospotActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            naviIcon = Shift.AsBool(Variable, 0x8000);
            flag = new SwitchFlag(Variable, 0x3F00);
            dialog = Shift.AsByte(Variable, 0x00FF);
        }
        protected override string GetActorName()
        {
            return "Navi Infospot";
        }
        protected override string GetVariable()
        {
            string dialogText;

            dialogText = ActorDialog.Get(0x011B, dialog);
            return String.Format("Type: {0}, Text: {1}, {2}",
                (naviIcon) ? "C-up" : "Pop-up",
                (dialogText.Length < TEXT_CAP) ? dialogText : dialogText.Substring(0,TEXT_CAP), 
                flag.ToString());
        }
    }
}
