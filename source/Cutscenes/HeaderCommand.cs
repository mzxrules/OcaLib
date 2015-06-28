using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    /// <summary>
    /// The header for a cutscene. Not a proper command, but contains frame information
    /// </summary>
    class HeaderCommand : CutsceneCommand, IFrameData
    {
        const int LENGTH = 8;
        public int Commands { get; private set; }
        public int EndFrame { get; private set; }

        public CutsceneCommand RootCommand
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
            get { return (short)EndFrame; }
            set { EndFrame = value; }
        }

        public HeaderCommand(BinaryReader br)
            : base(br.ReadBigUInt32(), br)
        {
            Load(br);
            Commands = (int)this.Command;
        }
        protected override int GetLength()
        {
            return LENGTH;
        }
        public override string ToString()
        {
            return String.Format("Header: Commands {0:X8}, End Frame {1:X8}",
                Command,
                EndFrame);
        }
        
        private void Load(BinaryReader br)
        {
            EndFrame = br.ReadBigInt32();
        }
        public override string ReadCommand()
        {
            return ToString();
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            yield return this;
        }
        public override void Save(BinaryWriter bw)
        {
            bw.WriteBig(Commands);
            bw.WriteBig(EndFrame);
        }
    }
}
