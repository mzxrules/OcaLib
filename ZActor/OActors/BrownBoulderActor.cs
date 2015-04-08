using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class BrownBoulderActor:ActorRecord
    {
        bool type;
        SwitchFlag flags;
        public BrownBoulderActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flags = (byte)Variable;
            type = ((Variable & 0x8000) > 0);
        }
        protected override string GetActorName()
        {
            return "Brown Boulder";
        }
        protected override string GetVariable()
        {
            return string.Format("{0}{1}",
                flags.ToString(),
                type ? " Puzzle Solved Sound" : "");
        }
    }
}
