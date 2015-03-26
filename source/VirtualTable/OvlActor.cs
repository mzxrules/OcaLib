using RHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class OvlActor
    {
        #region Structure
        /* 00: [Vrom] File Start, End
         * 08: [Vram] File Start, End
         * 10: [Ram]  File location
         * 14: [Vram] Actor Info?
         * 18: [Ram]  File name location
         * 1C: [?]    Unknown
         */

        //Main data structure
        public FileAddress Vrom;
        public FileAddress Vram;
        //uint ramFileStart;
        public uint VramActorInfo;
        public uint RamFileName;
        public uint Unknown;
        #endregion

        #region Extended Fields

        public const int LENGTH = 0x20;
        
        public FileAddress RamAddress
        {
            get { return _RamAddress; }
        }
        protected FileAddress _RamAddress;

        public int Actor
        {
            get { return _Actor; }
        }
        protected int _Actor;

        #endregion

        protected OvlActor() { }


        public OvlActor(int index, byte[] data)
        {
            Create(index, data, false);
        }

        protected OvlActor(int index, byte[] data, bool LittleWord)
        {
            Create(index, data, LittleWord);
        }

        private void Create(int index, byte[] data, bool LittleWord)
        {
            List<uint> d;
            if (LittleWord)
                d = GetData_Ram(data);
            else
                d = GetData_Rom(data);

            Create(index, d);
        }
        private void Create(int index, List<uint> data)
        {
            uint ramFileStart;
            _Actor = index;
            Vrom = new FileAddress(data[0], data[1]);
            Vram = new FileAddress(data[2], data[3]);

            ramFileStart = data[4];
            VramActorInfo = data[5];
            RamFileName = data[6];
            Unknown = data[7];

            _RamAddress = new FileAddress(ramFileStart, ramFileStart + Vrom.Size);
        }


        public static List<uint> GetData_Rom(byte[] data)
        {
            List<uint> d = new List<uint>();

            for (int i = 0; i < OvlActor.LENGTH; i += 4)
            {
                var v = BitConverter.ToUInt32(data, i);
                d.Add(Endian.ConvertUInt32(v));
            }
            return d;
        }

        public static List<uint> GetData_Ram(byte[] data)
        {
            List<uint> d = new List<uint>();

            for (int i = 0; i < OvlActor.LENGTH; i += 4)
            {
                var v = BitConverter.ToUInt32(data, i);
                d.Add(v);
            }
            return d;
        }

    }
}
