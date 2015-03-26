﻿using System;
using System.IO;
using System.Linq;

namespace mzxrules.OcaLib.Helper
{
    public enum FileEncoding
    {
        Error = 0,
        LittleEndian16,
        LittleEndian32,
        BigEndian32,
        HalfwordSwap,
    }

    public static class FileOrder
    {
        const int BUFFER_SIZE = 0x2000;

        public static void ToBigEndian32(Stream source, Stream output, FileEncoding sourceEncoding)
        {
            Endian.Order sourceEndian;
            Endian.Order outEndian;
            switch (sourceEncoding)
            {
                case FileEncoding.LittleEndian16: sourceEndian = Endian.Order.Little16; outEndian = Endian.Order.Big16; break;
                case FileEncoding.LittleEndian32: sourceEndian = Endian.Order.Little32; outEndian = Endian.Order.Big32; break;
                default: throw new NotImplementedException();
            }
            FileOrder.ConvertData(source, sourceEndian, output, outEndian);
        }

        /// <summary>
        /// Creates a copy of a rom with a given binary pattern
        /// </summary>
        /// <param name="inFile">the file to convert</param>
        /// <param name="inO">Endian order of the file</param>
        /// <param name="outFile">the stream to write the copy file</param>
        /// <param name="outO">Output file endian order</param>
        /// <returns>True if the copy was created. No copy will be created if inO == outO</returns>
        private static bool ConvertData(Stream inFile, Endian.Order inO, Stream outFile, Endian.Order outO)
        {
            Endian.Order outWord;

            outWord = (Endian.Order)((int)outO & 0x7F);

            if (inO == outO)
                return false;

            if (inO == Endian.Order.Big16 && outO == Endian.Order.Big32)
            {
                FlipDataByHalfword(inFile, outFile);
                return true;
            }

            //if word size does not match
            else if (((int)inO & 0x7F) != ((int)outO & 0x7F))
            {
                return false;
            }
            else
            {
                switch (outWord)
                {
                    case Endian.Order.Big16: FlipData(inFile, outFile, 2); break;
                    case Endian.Order.Big32: FlipData(inFile, outFile, 4); break;
                    default: return false;
                }
                return true;
            }
        }

        private static void FlipDataByHalfword(Stream source, Stream write)
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            for (int i = 0; i < source.Length; i += 4)
            {
                for (int j = 0; j < BUFFER_SIZE; j += 4)
                {
                    source.Read(buffer, j + 2, 2);
                    source.Read(buffer, j, 2);
                }
                write.Write(buffer, 0, BUFFER_SIZE);
            }
        }

        private static bool FlipData(Stream source, Stream write, int size)
        {
            byte[] word = new byte[size];
            byte[] buffer = new byte[BUFFER_SIZE];


            //FIXME: Out of bounds issues
            for (int i = 0; i < source.Length; i += BUFFER_SIZE)
            {
                source.Read(buffer, 0, BUFFER_SIZE);
                for (int j = 0; j < BUFFER_SIZE; j += size)
                {
                    Array.Copy(buffer, j, word, 0, size);
                    write.Write(word.Reverse().ToArray(), 0, size);
                }
            }
            return true;
        }
    }
}