namespace mzxrules.Helper
{
    public class SegmentAddress
    {
        public byte Segment { get; set; }
        public int Offset { get; set; }

        public SegmentAddress(int addr)
        {
            Segment = (byte)(addr >> 24);
            Offset = addr & 0xFFFFFF;
        }
        public SegmentAddress(uint addr)
        {
            Segment = (byte)(addr >> 24);
            Offset = (int)(addr & 0xFFFFFF);
        }

        public static implicit operator SegmentAddress(int ptr)
        {
            return new SegmentAddress(ptr);
        }

        public static implicit operator SegmentAddress(uint ptr)
        {
            return new SegmentAddress(ptr);
        }
    }
}
