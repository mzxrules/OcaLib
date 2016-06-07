using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.Cutscenes
{
    public class CutsceneCommand : AbstractCutsceneCommand
    {
        public IEnumerable<IFrameData> IFrameDataEnum { get { return GetIFrameDataEnumerator(); } }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            if (false)
                yield return null;
        }

        protected CutsceneCommand(int command, BinaryReader br)
        {
            this.Command = command;
            Index = br.BaseStream.Position - 4;
        }
        protected CutsceneCommand() { }

        public override string ReadCommand()
        {
            throw new InvalidOperationException();
        }

        protected override int GetLength()
        {
            throw new InvalidOperationException();
        }

        public override void Save(BinaryWriter bw)
        {
            throw new InvalidOperationException();
        }

        public override void AddEntry(IFrameData item)
        {
            throw new InvalidOperationException();
        }

        public override void RemoveEntry(IFrameData item)
        {
            throw new InvalidOperationException();
        }
    }
}
