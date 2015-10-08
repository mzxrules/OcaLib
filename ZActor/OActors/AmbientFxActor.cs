using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class AmbientFxActor : ActorRecord_Wrapper
    {
        byte type;
        public AmbientFxActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (byte)Variable;
        }
        protected override string GetActorName()
        {
            return "Ambient Sound";
        }
        protected override string GetVariable()
        {
            string typeStr;
            switch (type)
            {
                case 0x0000: typeStr = "Nothing"; break;
                case 0x0001: typeStr = "Stream"; break;
                case 0x0002: typeStr = "Magma"; break;
                case 0x0003: typeStr = "Waterfall"; break;
                case 0x0004: typeStr = "Small stream"; break;
                case 0x0005: typeStr = "Stream"; break;
                case 0x0006: typeStr = "Fire Temple’s lower ambient noise"; break;
                case 0x0007: typeStr = "Fire Temple’s higher ambient noise"; break;
                case 0x0008: typeStr = "Dripping noise (Well)"; break;
                case 0x0009: typeStr = "River"; break;
                case 0x000A: typeStr = "Market gibberish"; break;
                case 0x000B: typeStr = "??"; break;
                case 0x000D: typeStr = "Proximity Saria’s Song"; break;
                case 0x000E: typeStr = "Howling wind"; break;
                case 0x000F: typeStr = "Gurgling"; break;
                case 0x0010: typeStr = "Temple of Light’s dripping sounds"; break;
                case 0x0011: typeStr = "Low booming-likish sound"; break;
                case 0x0012: typeStr = "Quake/Collapse"; break;
                case 0x0013: typeStr = "Fairy Fountain"; break;
                case 0x0014: typeStr = "Torches"; break;
                case 0x0015: typeStr = "Cows"; break;
                default: typeStr = "Unknown"; break;
            }
            return typeStr;
        }
    }
}
