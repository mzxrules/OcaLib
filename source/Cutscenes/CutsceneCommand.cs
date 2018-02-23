using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.Cutscenes
{
    public class CutsceneCommand 
    {
        public Int32 Command { get; protected set; }
        public long Index { get; protected set; }
        public int Length { get { return GetLength(); } }

        protected CutsceneCommand(int command, BinaryReader br, long index)
        {
            Command = command;
            Index = index;
            //Index = br.BaseStream.Position - 4;
        }
        protected CutsceneCommand() { }

        public virtual string ReadCommand()
        {
            throw new InvalidOperationException();
        }

        protected virtual int GetLength()
        {
            throw new InvalidOperationException();
        }

        public virtual void Save(BinaryWriter bw)
        {
            throw new InvalidOperationException();
        }
    }
}
