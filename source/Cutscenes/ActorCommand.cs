using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    class ActorCommand : CutsceneCommand
    {
        const int LENGTH = 8;
        List<ActorCommandEntry> Entries = new List<ActorCommandEntry>();

        public ActorCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }
        
        private void Load(BinaryReader br)
        {
            int EntryCount;
            EntryCount = br.ReadBigInt32();

            for (int i = 0; i < EntryCount; i++)
            {
                Entries.Add(new ActorCommandEntry(this, br));
            }
        }

        public override string ToString()
        {
            return String.Format("{0:X8}: Actor, Entries: {1:X8}", Command, Entries.Count);
        }

        public override string ReadCommand()
        {
            StringBuilder sb;
            sb = new StringBuilder();

            sb.AppendLine(ToString());
            foreach (ActorCommandEntry e in Entries)
                sb.AppendLine("   " + e.ToString());
            return sb.ToString();
        }

        protected override int GetLength()
        {
            return Entries.Count * ActorCommandEntry.LENGTH + LENGTH;
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in Entries)
                yield return fd;
        }
    }
}
