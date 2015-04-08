using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZActor.OActors
{
    public class NaviInfospotActor : ActorRecord
    {
        const int TEXT_CAP = 80;
        bool naviIcon;
        SwitchFlag flag;
        byte dialog;
        public NaviInfospotActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            naviIcon = Pack.AsBool(Variable, 0x8000);
            flag = new SwitchFlag(Variable, 0x3F00);
            dialog = Pack.AsByte(Variable, 0x00FF);
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
