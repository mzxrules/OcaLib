using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RHelper;

namespace mzxrules.OcaLib.Cutscenes
{
    class TextCommand : CutsceneCommand
    {
        const int LENGTH = 8;
        int entryCount;
        List<TextCommandEntry> entries = new List<TextCommandEntry>();

        public TextCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }
        protected override int GetLength()
        {
            return TextCommandEntry.LENGTH * entries.Count + LENGTH;
        }
        
        private void Load(BinaryReader br)
        {
            byte[] arr;
            arr = br.ReadBytes(sizeof (int));
            Endian.Convert(out entryCount, arr);

            for (int i = 0; i < entryCount; i++)
            {
                entries.Add(new TextCommandEntry());
                entries[i].Load(this, br);
            }
        }
        public override string ToString()
        {
            return String.Format("{0:X8}: Text Command, {1} entries",
                Command,
                entryCount);
        }
        public override string ReadCommand()
        {
            StringBuilder r = new StringBuilder();

            r.AppendLine(ToString());
            foreach (TextCommandEntry e in entries)
                r.AppendLine("   " + e.ToString());

            return r.ToString();
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in entries)
                yield return fd;
        }

        class TextCommandEntry : IFrameData
        {
            public const int LENGTH = 12;
            ushort text;
            public CutsceneCommand RootCommand { get; set; }
            public short StartFrame { get; set; }
            public short EndFrame { get; set; }
            ushort a, b, c;
            internal void Load(CutsceneCommand cmd, BinaryReader br)
            {
                short startFrame;
                short endFrame;
                byte[] arr;
                arr = br.ReadBytes(sizeof(short) * 6);

                Endian.Convert(out text, arr, 0);
                Endian.Convert(out startFrame, arr, 2);
                Endian.Convert(out endFrame, arr, 4);
                Endian.Convert(out a, arr, 6);
                Endian.Convert(out b, arr, 8);
                Endian.Convert(out c, arr, 10);

                RootCommand = cmd;
                StartFrame = startFrame;
                EndFrame = endFrame;
            }
            public override string ToString()
            {
                return String.Format("{0}, Start: {1:X4}, End: {2:X4}, {3:X4} {4:X4} {5:X4}",
                    (text != 0xFFFF)? "Text " + text.ToString("X4"): "No Text",
                    StartFrame,
                    EndFrame,
                    a,
                    b,
                    c);
            }
        }
    }
}
