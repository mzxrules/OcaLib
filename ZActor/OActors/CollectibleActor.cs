namespace mzxrules.ZActor.OActors
{
    class CollectibleActor:ActorRecord_Wrapper
    {
        byte type;
        CollectableFlag flags;
        public CollectibleActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (byte)Variable;
            flags = (byte)(Variable >> 8);
        }
        protected override string GetActorName()
        {
            string typeStr;
            switch (type)
            {
                case 0x00: typeStr = "Green Rupee"; break;
                case 0x01: typeStr = "Blue rupee"; break;
                case 0x02: typeStr = "Red rupee"; break;
                case 0x03: typeStr = "Heart"; break;
                case 0x04: typeStr = "Bomb"; break;
                case 0x05: typeStr = "Arrow"; break;
                case 0x06: typeStr = "Heart Piece"; break;
                case 0x07: typeStr = "Heart container (Alpha)"; break;
                case 0x08: typeStr = "Arrow"; break;
                case 0x09: typeStr = "Double arrow"; break;
                case 0x0a: typeStr = "Triple arrow"; break;
                case 0x0b: typeStr = "Bomb (5)"; break;
                case 0x0c: typeStr = "Deku nut"; break;
                case 0x0d: typeStr = "Deku stick"; break;
                case 0x0e: typeStr = "Large Magic jar"; break;
                case 0x0f: typeStr = "Magic jar"; break;
                case 0x10: typeStr = "Deku seeds"; break;
                case 0x11: typeStr = "Small Key"; break;
                case 0x12: typeStr = "Invisible heart"; break;
                case 0x13: typeStr = "Giant orange rupee"; break;
                case 0x14: typeStr = "Large purple rupee"; break;
                case 0x19: typeStr = "Bomb (5)"; break;
                default: typeStr = "Unknown"; break;
            }
            return "Collectable " + typeStr;
        }
        protected override string GetVariable()
        {
            return flags.ToString();
        }
    }
}
