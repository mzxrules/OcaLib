using RHelper;
using System;

namespace mzxrules.OcaLib.SceneRoom
{
    public class SceneWord
    {
        public const int LENGTH = 8; 
        byte[] cmd;
        public byte Code { get { return cmd[0]; } set { cmd[0] = value; } }
        public byte Data1 { get { return cmd[1]; } set { cmd[1] = value; } }
        public UInt32 Data2
        {
            get { return Endian.ConvertUInt32(cmd, 4); }
        }

        public byte this[int i]
        {
            get
            {
                //This indexer is very simple, and just returns or sets 
                //the corresponding element from the internal array.
                return cmd[i];
            }
            set
            {
                cmd[i] = value;
            }
        }

        public SceneWord()
        {
            cmd = new byte[8];
        }

        private SceneWord(byte[] arr)
        {
            cmd = arr;
        }

        public static implicit operator byte[](SceneWord cmd)
        {
            return cmd.cmd;
        }
        public static implicit operator SceneWord(byte[] arr)
        {
            return new SceneWord(arr);
        }
    }
}