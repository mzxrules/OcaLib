namespace mzxrules.Helper
{
    public class SegmentAddress
    {
        public byte Segment
        {
            get { return (byte)(Address >> 24); }
            set { Address = (value << 24) | (Address & 0xFFFFFF); }
        }
        public int Offset
        {
            get { return Offset = Address & 0xFFFFFF; }
            set { Address = (int)((uint)Address & 0xFF000000) | (value & 0xFFFFFF); }
        }

        private int Address;

        public SegmentAddress(int addr)
        {
            Address = addr;
        }
        public SegmentAddress(uint addr)
        {
            Address = (int)addr;
        }

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
            return Address.ToString("X8");
        }
    }
}
