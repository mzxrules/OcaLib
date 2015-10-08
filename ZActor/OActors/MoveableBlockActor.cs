using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class MoveableBlockActor:ActorRecord_Wrapper
    {
        SwitchFlag flag;
        byte color;
        bool risingEdge;
        byte size;
        public MoveableBlockActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = (byte)((Variable & 0x3F00) >> 8);
            color = (byte)((Variable & 0xF0) >> 4);
            risingEdge = ((Variable & 0x04) > 0);
            size = (byte)(Variable & 0x03);
        }
        protected override string GetActorName()
        {
            string sizeStr;
            string colorStr;
            switch (color)
            {
                case 0x0: colorStr = "Red"; break;
                case 0x4: colorStr = "Green"; break;
                default: colorStr = "Color" + color.ToString(); break;
            }
            switch (size)
            {
                case 0x0: sizeStr = "Small"; break;
                case 0x1: sizeStr = "Large"; break;
                case 0x2: sizeStr = "Very large"; break;
                case 0x3: sizeStr = "Huge"; break;
                default: sizeStr = ""; break; ;
            }
            return string.Format("{0} {1} Block",
                sizeStr,
                colorStr);
        }
        protected override string GetVariable()
        {
            return string.Format("Bound to {0}, load when {1}",
                flag.ToString(),
                risingEdge ? "set" : "unset");
        }
    }
}
