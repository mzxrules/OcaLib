using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class GraveyardActor:ActorRecord
    {
        byte type;
        SwitchFlag flags;
        public GraveyardActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flags = (byte)(Variable >> 8);
            type = (byte)(Variable);
        }
        protected override string GetActorName()
        {
            switch (type)
            {
                case 0x00: return "Shadow Temple Blocking Eye Wall";
                case 0x01: return "Solid Grass (Hides Dampe's Grave)";
                case 0x02: return "Royal Tomb Gravestone";
                default: return "Graveyard Actor";
            }
        }
        protected override string GetVariable()
        {
            switch (type)
            {
                case 0: return flags.ToString();
                default: return base.GetVariable();
            }
        }
    }
}
