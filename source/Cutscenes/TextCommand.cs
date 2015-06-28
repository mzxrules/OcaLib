﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    class TextCommand : CutsceneCommand
    {
        const int LENGTH = 8;
        List<TextCommandEntry> Entries = new List<TextCommandEntry>();

        public TextCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }
        protected override int GetLength()
        {
            return TextCommandEntry.LENGTH * Entries.Count + LENGTH;
        }
        
        private void Load(BinaryReader br)
        {
            int entryCount;

            entryCount = br.ReadBigInt32();

            for (int i = 0; i < entryCount; i++)
            {
                Entries.Add(new TextCommandEntry());
                Entries[i].Load(this, br);
            }
        }
        public override void Save(BinaryWriter bw)
        {
            bw.WriteBig(Command);
            bw.WriteBig((Int32)Entries.Count);
            foreach (TextCommandEntry item in Entries)
                item.Save(bw);
        }

        public override string ToString()
        {
            return String.Format("{0:X8}: Text Command, {1} entries",
                Command,
                Entries.Count);
        }
        public override string ReadCommand()
        {
            StringBuilder r = new StringBuilder();

            r.AppendLine(ToString());
            foreach (TextCommandEntry e in Entries)
                r.AppendLine("   " + e.ToString());

            return r.ToString();
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in Entries)
                yield return fd;
        }
    }
}
