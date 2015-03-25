using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RHelper;

namespace OcarinaPlayer.Cutscenes
{
    class HeaderCommand : CutsceneCommand, IFrameData
    {
        const int LENGTH = 8;
        public int Commands { get { return (int)command; } }
        public int EndFrame { get { return endFrame; } }
        int endFrame;

        public AbstractCutsceneCommand RootCommand
        {
            get { return this; }
            set { throw new InvalidOperationException(); }
        }

        public short StartFrame
        {
            get { return 0; }
            set { throw new InvalidOperationException(); }
        }

        short IFrameData.EndFrame
        {
            get { return (short)endFrame; }
            set { endFrame = value; }
        }

        public HeaderCommand(BinaryReader br)
            : base(br.ReadBigUInt32(), br)
        {
            
        }
        protected override int GetLength()
        {
            return LENGTH;
        }
        public override string ToString()
        {
            return String.Format("Header: Commands {0:X8}, End Frame {1:X8}",
                command,
                endFrame);
        }
        protected override void Load(BinaryReader br)
        {
            endFrame = br.ReadBigInt32();
        }
        public override string ReadCommand()
        {
            return ToString();
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            yield return this;
        }

    }
}
