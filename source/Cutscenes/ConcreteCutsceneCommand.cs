using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OcarinaPlayer.Cutscenes
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
            this.command = command;
            index = br.BaseStream.Position;
            Load(br);
        }

        protected override void Load(BinaryReader br)
        {
            throw new InvalidOperationException();
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
