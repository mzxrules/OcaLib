using System;
namespace mzxrules.Helper
{
    public struct N64Ptr : IEquatable<N64Ptr>, IComparable<N64Ptr>
    {
        private long Pointer;

        public N64Ptr(long ptr)
        {
            Pointer = (ptr << 32) >> 32;
        }
        
        public static implicit operator N64Ptr(int ptr)
        {
            return new N64Ptr(ptr);
        }

        public static implicit operator N64Ptr(uint ptr)
        {
            return new N64Ptr(ptr);
        }

        public static implicit operator N64Ptr(long ptr)
        {
            return new N64Ptr(ptr);
        }

        public static explicit operator uint(N64Ptr ptr)
        {
            return (uint)ptr.Pointer;
        }
        public static implicit operator int(N64Ptr ptr)
        {
            return (int)ptr.Pointer;
        }

        public static bool operator ==(N64Ptr a, N64Ptr b)
        {
            return a.Pointer == b.Pointer;
        }

        public static bool operator != (N64Ptr a, N64Ptr b)
        {
            return !(a == b);
        }

        public static bool operator > (N64Ptr a, N64Ptr b)
        {
            return a.CompareTo(b) == 1;
        }

        public static bool operator < (N64Ptr a, N64Ptr b)
        {
            return a.CompareTo(b) == -1;
        }

        public static bool operator >= (N64Ptr a, N64Ptr b)
        {
            return a.CompareTo(b) >= 0;
        }

        public static bool operator <= (N64Ptr a, N64Ptr b)
        {
            return a.CompareTo(b) <= 0;
        }

        public override bool Equals(object obj)
        {
            return obj is N64Ptr && this == (N64Ptr)obj;
        }

        public override int GetHashCode()
        {
            return Pointer.GetHashCode();
        }

        public override string ToString()
        {
            return $"{(int)Pointer:X8}";
        }

        public int Base()
        {
            return (int)Pointer & 0xFFFFFF;
        }

        public bool Equals(N64Ptr other)
        {
            return Pointer == other.Pointer;
        }

        public int CompareTo(N64Ptr other)
        {
            return ((uint)Pointer).CompareTo((uint)other.Pointer);
        }
    }
}
