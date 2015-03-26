using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    class ExitCommand : CutsceneCommand
    {
        const int LENGTH = 8;
        int entryCount;
        List<ExitCommandEntry> entries = new List<ExitCommandEntry>();

        public ExitCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }

        protected override int GetLength()
        {
            return ExitCommandEntry.LENGTH * entries.Count + LENGTH;
        }
        
        private void Load(BinaryReader br)
        {
            Endian.Convert(out entryCount, br.ReadBytes(sizeof(int)));

            if (entryCount > 20)
                throw new ArgumentOutOfRangeException("Too many 3E8 command entries");
            for (int i = 0; i < entryCount; i++)
            {
                entries.Add(new ExitCommandEntry());
                entries[i].Load(this, br);
            }
        }
        public override string ReadCommand()
        {
            StringBuilder r;
            r = new StringBuilder();

            r.AppendLine(ToString());
            foreach (ExitCommandEntry e in entries)
                r.AppendLine("   " + e.ToString());
            return r.ToString();
        }

        public override string ToString()
        {
            return String.Format("{0:X8}: Exit Command, {1} entries",
                Command,
                entryCount);
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in entries)
                yield return fd;
        }

        class ExitCommandEntry : IFrameData
        {
            public const int LENGTH = 8;
            ushort asm;
            public short StartFrame { get; set; }
            public short EndFrame { get; set; }
            public CutsceneCommand RootCommand { get; set; }
            short endFrame2;

            internal void Load(CutsceneCommand cmd, BinaryReader br)
            {
                RootCommand = cmd;
                Endian.Convert(out asm, br.ReadBytes(2));
                StartFrame = br.ReadBigInt16();
                EndFrame = br.ReadBigInt16();
                Endian.Convert(out endFrame2, br.ReadBytes(2));
            }
            public override string ToString()
            {
                return string.Format("asm: {0:X4}, start: {1:X4}, end: {2:X4}, {3:X4}",
                    asm,
                    StartFrame,
                    EndFrame,
                    endFrame2);
            }
        }
    }
}
