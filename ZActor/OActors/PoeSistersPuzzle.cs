using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class PoeSistersPuzzle : ActorRecord
    {
        SwitchFlag flag;
        byte type;
        public PoeSistersPuzzle(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = Pack.AsByte(Variable, 0xFF00);
            flag = Pack.AsByte(Variable, 0x003F);
        }
        protected override string GetActorName()
        {
            return "Poe Sisters Puzzle";
        }
        //FIXME: fsf
        protected override string GetVariable()
        {
            string typeStr;
            switch (type)
            {
                case 00: typeStr = "??? Amy"; break;
                case 01: typeStr = "??? Amy"; break;
                case 02: typeStr = "Joelle Painting"; break;
                case 03: typeStr = "Beth Painting"; break;
                case 04: typeStr = "??? Amy"; break;
                default: typeStr = "???"; break;
            }
            return string.Format("Type: {0}, {1}",
                typeStr,
                flag);
        }
    }
}