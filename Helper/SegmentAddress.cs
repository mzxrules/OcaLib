namespace mzxrules.Helper
{
    public struct SegmentAddress
    {
        public byte Segment
        {
            get { return (byte)(value >> 24); }
        }
        public int Offset
        {
            get { return value & 0xFFFFFF; }
        }

        private int value;

        public SegmentAddress(int addr)
        {
            value = addr;
        }
        public SegmentAddress(uint addr)
        {
            value = (int)addr;
        }
        public SegmentAddress(byte bank, int offset)
        {
            value = value = (bank << 24) | (offset & 0xFFFFFF);
        }
        public SegmentAddress(SegmentAddress seg, int offset) : this(seg.Segment, offset) { }

        public static implicit operator SegmentAddress(int ptr)
        {
            return new SegmentAddress(ptr);
        }

        public static implicit operator SegmentAddress(uint ptr)
        {
            return new SegmentAddress(ptr);
        }

        public override string ToString()
        {
            return $"{value:X8}";
        }
    }
}
