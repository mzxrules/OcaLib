using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mzxrules.OcaLib.Cutscenes
{
    class CutsceneCommand : AbstractCutsceneCommand
    {
        public IEnumerable<IFrameData> IFrameDataEnum { get { return GetIFrameDataEnumerator(); } }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            if (false)
                yield return null;
        }

        protected CutsceneCommand(uint command, BinaryReader br)
        {
            this.Command = command;
            Index = br.BaseStream.Position;
            //Load(br);
        }

        public override string ReadCommand()
        {
            throw new InvalidOperationException();
        }

        protected override int GetLength()
        {
            throw new InvalidOperationException();
        }

    }
}
