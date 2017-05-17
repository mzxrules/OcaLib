using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.Cutscenes
{
    //public interface ILength
    //{
    //    int Length { get; }
    //}

    public class CutsceneCommand // : AbstractCutsceneCommand
    {
        //protected abstract IEnumerable<IFrameData> GetIFrameDataEnumerator();
        public Int32 Command { get; protected set; }
        public long Index { get; protected set; }
        public int Length { get { return GetLength(); } }

        //public IEnumerable<IFrameData> IFrameDataEnum { get { return GetIFrameDataEnumerator(); } }

        //protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        //{
        //    if (false)
        //        yield return null;
        //}

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
