using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.Helper
{
    public class N64Ptr
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

        public static implicit operator uint(N64Ptr ptr)
        {
            return (uint)ptr.Pointer;
        }

        public static bool operator ==(N64Ptr a, N64Ptr b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Pointer == b.Pointer;
        }

        public static bool operator != (N64Ptr a, N64Ptr b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            N64Ptr p = obj as N64Ptr;
            if ((object)p == null)
                return false;

            return Pointer == p.Pointer;
        }

        public override int GetHashCode()
        {
            return Pointer.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("0x{0:X8}", (uint)Pointer);
        }
    }
}
