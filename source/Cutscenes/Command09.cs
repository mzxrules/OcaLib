using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    public class Command09 : CutsceneCommand, IFrameCollection
    {
        const int LENGTH = 8;
        List<ThisEntry> entries = new List<ThisEntry>();
        public Command09(int command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }

        private void Load(BinaryReader br)
        {
            int entryCount = br.ReadBigInt32();
            
            for (int i = 0; i < entryCount; i++)
            {
                entries.Add(new ThisEntry(this, br));
            }

        }
        public override void Save(BinaryWriter bw)
        {
            bw.WriteBig(Command);
            bw.WriteBig(entries.Count);
            foreach (var item in entries)
                item.Save(bw);
        }

        public override string ToString()
        {
            return String.Format("{0:X4} entries {1:X8}",
                Command,
                entries.Count);
        }

        public override string ReadCommand()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(ToString());

            foreach (ThisEntry ent in entries)
            {
                sb.AppendLine(ent.ToString());
            }
            return sb.ToString();
        }

        protected override int GetLength()
        {
            return entries.Count * ThisEntry.LENGTH + LENGTH;
        }

        public override void AddEntry(IFrameData item)
        {
            entries.Add((ThisEntry)item);
        }

        public override void RemoveEntry(IFrameData item)
        {
            entries.Remove((ThisEntry)item);
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in entries)
                yield return fd;
        }

        class ThisEntry : IFrameData
        {
            public CutsceneCommand RootCommand { get; set; }
            public short StartFrame { get; set; }
            public short EndFrame { get; set; }
            public const int LENGTH = 12;
            ushort int1;
            ushort s4;
            ushort s5;
            ushort s6;

            public ThisEntry(CutsceneCommand cmd, BinaryReader br)
            {
                RootCommand = cmd;
                int1 = br.ReadBigUInt16();
                StartFrame = br.ReadBigInt16();
                EndFrame = br.ReadBigInt16();

                s4 = br.ReadBigUInt16();
                s5 = br.ReadBigUInt16();
                s6 = br.ReadBigUInt16();
            }

            internal void Save(BinaryWriter bw)
            {
                bw.WriteBig(int1);
                bw.WriteBig(StartFrame);
                bw.WriteBig(EndFrame);
                bw.WriteBig(s4);
                bw.WriteBig(s5);
                bw.WriteBig(s6);
            }

            public override string ToString()
            {
                return string.Format("{0:X4} Frame Start: {1:X4} Frame End {2:X4} {3:X4} {4:X4} {5:X4}",
                    int1,
                    StartFrame,
                    EndFrame,
                    s4,
                    s5,
                    s6);
            }
        }
    }
}
