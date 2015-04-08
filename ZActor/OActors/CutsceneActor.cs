using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class CutsceneActor : ActorRecord
    {
        byte type;
        public CutsceneActor(byte[] record, params int[] p)
            : base(record)
        {
            type = (byte)(Variable >> 8);
        }
        protected override string GetActorName()
        {
            switch (type)
            {
                case 00: return "Impa's Horse";
                case 01: return "Impa";
                case 02: return "Child Zelda";
                case 03: return "Ganondorf sitting on horse";
                case 04: return "Ganondorf's Horse (Standing)";
                case 05: return "Ganondorf riding on horse  with flames";
                case 06: return "Ganodorf's Horse (Galloping)";
                case 07: return "Ganodorf, standing hands crossed";
                case 08: return "Ganondorf bowing to the King";
                case 09: return "Ganondorf, floating during CURSE YOU";
                default: return "Cutscene Actor";
            }
        }
    }
}
