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
        int EntryCount;
        List<ExitCommandEntry> Entries = new List<ExitCommandEntry>();

        public ExitCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }
        
        private void Load(BinaryReader br)
        {
            EntryCount = br.ReadBigInt32();

            if (EntryCount > 20)
                throw new ArgumentOutOfRangeException("Too many 3E8 command entries");
            for (int i = 0; i < EntryCount; i++)
            {
                Entries.Add(new ExitCommandEntry());
                Entries[i].Load(this, br);
            }
        }
        public override void Save(BinaryWriter bw)
        {
            bw.WriteBig(Command);
            bw.WriteBig(EntryCount);
            foreach (ExitCommandEntry item in Entries)
                item.Save(bw);
        }

        public override string ReadCommand()
        {
            StringBuilder r;
            r = new StringBuilder();

            r.AppendLine(ToString());
            foreach (ExitCommandEntry e in Entries)
                r.AppendLine("   " + e.ToString());
            return r.ToString();
        }

        public override string ToString()
        {
            return String.Format("{0:X8}: Exit Command, {1} entries",
                Command,
                EntryCount);
        }

        protected override int GetLength()
        {
            return ExitCommandEntry.LENGTH * Entries.Count + LENGTH;
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in Entries)
                yield return fd;
        }

        class ExitCommandEntry : IFrameData
        {
            public const int LENGTH = 8;
            ushort Asm;
            public short StartFrame { get; set; }
            public short EndFrame { get; set; }
            public CutsceneCommand RootCommand { get; set; }
            short endFrame2;

            internal void Load(CutsceneCommand cmd, BinaryReader br)
            {
                RootCommand = cmd;
                Asm = br.ReadBigUInt16();
                StartFrame = br.ReadBigInt16();
                EndFrame = br.ReadBigInt16();
                endFrame2 = br.ReadBigInt16();
            }
            public void Save(BinaryWriter bw)
            {
                bw.WriteBig(Asm);
                bw.WriteBig(StartFrame);
                bw.WriteBig(EndFrame);
                bw.WriteBig(endFrame2);
            }

            public override string ToString()
            {
                return string.Format("asm: {0:X4}, start: {1:X4}, end: {2:X4}, {3:X4}",
                    Asm,
                    StartFrame,
                    EndFrame,
                    endFrame2);
            }
        }
    }
}
