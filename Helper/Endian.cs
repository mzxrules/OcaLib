using System;

namespace mzxrules.OcaLib.Helper
{
    public static class Endian
    {
        public enum Order
        {
            Big16 = 0,
            Big32 = 1,
            Big64 = 2,
            Little16 = 128,
            Little32 = 129,
            Little64 = 130,
        }

        /// <summary>
        /// Reverses every "word" (4 bytes) within a sequence of bytes
        /// </summary>
        /// <param name="data"></param>
        public static void Reverse32(this byte[] data)
        {
            if (data.Length % 4 != 0)
                throw new InvalidOperationException();
            ReverseBytes(ref data, 4);
        }

        public static void ReverseBytes(ref byte[] inArray)
        {
            ReverseBytes(ref inArray, inArray.Length);
        }
        public static void ReverseBytes(ref byte[] inArray, int typeSize)
        {
            byte temp;

            for (int i = 0; i < inArray.Length; i += typeSize)
            {
                int highCtr = i + typeSize - 1;

                for (int ctr = 0; ctr < typeSize / 2; ctr++)
                {
                    temp = inArray[ctr + i];
                    inArray[ctr + i] = inArray[highCtr];
                    inArray[highCtr] = temp;
                    highCtr -= 1;
                }
            }
        }

        
        #region Int16

        public static void Convert(ref short v)
        {
            v = ConvertShort(v);
        }
        public static void Convert(out short result, byte[] array, int offset = 0)
        {
            result = ConvertShort(array, offset);
        }
        public static Int16 ConvertInt16(Int16 v)
        {
            return ConvertShort(v);
        }
        public static short ConvertShort(short v)
        {
            return (short)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
        }

        public static Int16 ConvertInt16(byte[] array, int offset)
        {
            return ConvertShort(array, offset);
        }
        public static short ConvertShort(byte[] array, int offset)
        {
            return (short)((array[offset] << 8) | array[offset + 1]);
        }

        #endregion

        #region UInt16
        public static void Convert(out ushort result, byte[] array, int offset = 0)
        {
            result = ConvertUShort(array, offset);
        }

        public static UInt16 ConvertUInt16(byte[] array, int offset)
        {
            return ConvertUShort(array, offset);
        }
        public static ushort ConvertUShort(byte[] array, int offset)
        {
            return (ushort)((array[offset] << 8) + array[offset + 1]);
        }

        public static void Convert(ref ushort v)
        {
            v = ConvertUShort(v);
        }
        public static UInt16 ConvertUInt16(ushort v)
        {
            return ConvertUShort(v);
        }
        public static ushort ConvertUShort(ushort v)
        {
            return (ushort)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
        }

        #endregion

        #region Int32
        public static void Convert(out int result, byte[] array, int offset = 0)
        {
            result = ConvertInt32(array, offset);
        }

        public static Int32 ConvertInt32(byte[] array, int offset)
        {
            return (array[offset] << 24) | (array[offset + 1] << 16) | (array[offset + 2] << 8) | array[offset + 3];
        }

        public static void Convert(ref Int32 v)
        {
            v = ConvertInt32(v);
        }

        public static Int32 ConvertInt32(Int32 value)
        {
            return ((value & 0xFF) << 24) | ((value & 0xFF00) << 8) | ((value >> 8) & 0xFF00) | ((value >> 24) & 0xFF);
        }
        #endregion

        #region UInt32
        public static void Convert(out UInt32 result, byte[] array, int offset = 0)
        {
            result = ConvertUInt32(array, offset);
        }

        public static UInt32 ConvertUInt32(byte[] array, int offset)
        {
            return (uint)ConvertInt32(array, offset);
        }

        public static void Convert(ref UInt32 v)
        {
            v = ConvertUInt32(v);
        }

        public static UInt32 ConvertUInt32(UInt32 value)
        {
            return ((value & 0xFF) << 24) | ((value & 0xFF00) << 8) | ((value >> 8) & 0xFF00) | ((value >> 24) & 0xFF);
        }

        #endregion

        public static void Convert(ref UInt64 v)
        {
            v = ConvertUInt64(v);
        }

        private static UInt64 ConvertUInt64(ulong v)
        {
            return ConvertUInt32((UInt32)((v >> 32) & 0xFFFFFFFF)) | ((UInt64)ConvertUInt32((UInt32)v) << 32);
        }

        public static void Convert(Vector3<uint> v, byte[] arr, int offset)
        {
            Endian.Convert(out v.x, arr, offset + 0);
            Endian.Convert(out v.y, arr, offset + 4);
            Endian.Convert(out v.z, arr, offset + 8);
        }
        public static void Convert(Vector3<int> v, byte[] arr, int offset)
        {
            Endian.Convert(out v.x, arr, offset + 0);
            Endian.Convert(out v.y, arr, offset + 4);
            Endian.Convert(out v.z, arr, offset + 8);
        }
        public static void Convert(Vector3<float> v, byte[] arr, int offset)
        {
            byte[] arr2 = new byte[0x0C];

            Array.Copy(arr, offset, arr2, 0, 0x0C);
            ReverseBytes(ref arr2, 0x04);

            v.x = BitConverter.ToSingle(arr2, 0);
            v.y = BitConverter.ToSingle(arr2, 4);
            v.z = BitConverter.ToSingle(arr2, 8);
            
        }

    }
}
