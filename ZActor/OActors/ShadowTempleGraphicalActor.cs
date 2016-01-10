namespace mzxrules.ZActor.OActors
{
    class ShadowTempleGraphicalActor : ActorRecord_Wrapper
    {
        byte type;
        public ShadowTempleGraphicalActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = (byte)Variable;
        }
        protected override string GetActorName()
        {
            switch (type)
            {
                case 0x03: return "Shadow Temple Rock Wall with Skull";
                case 0x04: return "Shadow Temple Black square with large skull face";
                case 0x05: return "Shadow Temple Boss Room platforms";
                case 0x06: return "Shadow Temple Wall of Skulls";
                case 0x07: return "Shadow Temple Floor (bluish? texture)";
                case 0x08: return "Shadow Temple Massive Platform";
                case 0x09: return "Shadow Temple Wall with bluish?, fat bricks texture (one sided)";
                case 0x0A: return "Shadow Temple Diamond Room (before big key) Fake Walls";
                case 0x0B: return "Shadow Temple Wall with purplish?, fat brick texture (both side)";
                case 0x0C: return "Shadow Temple Map 11's invisible spikes, invisible hookshot point.";
                default: return "Unknown Shadow Temple Graphical";
            }
        }
    }
}
