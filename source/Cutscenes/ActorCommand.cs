using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    public class ActorCommand : CutsceneCommand, IFrameCollection
    {
        const int LENGTH = 8;
        public List<ActionEntry> Entries = new List<ActionEntry>();

        public ActorCommand(int command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }
        
        public ActorCommand(int command, ActionEntry entry)
        {
            Command = command;
            entry.RootCommand = this;
            Entries.Add(entry);
        }
        
        private void Load(BinaryReader br)
        {
            int EntryCount;
            EntryCount = br.ReadBigInt32();

            for (int i = 0; i < EntryCount; i++)
            {
                Entries.Add(new ActionEntry(this, br));
            }
        }

        public override void Save(BinaryWriter bw)
        {
            bw.WriteBig(Command);
            bw.WriteBig(Entries.Count);
            foreach (ActionEntry item in Entries)
                item.Save(bw);
        }

        public override void RemoveEntry(IFrameData i)
        {
            Entries.Remove((ActionEntry)i);
        }

        public override string ToString()
        {
            return string.Format("{0:X4}: {2}, Entries: {1:X8}",
                Command, Entries.Count, GetActorName());
        }

        private string GetActorName()
        {
            switch (Command)
            {
                case 0x03: return "Title Logo";
                case 0x04: return "Environment Settings";
                case 0x0A: return "Link";
                case 0x27: return "Rauru (08)";
                case 0x29: return "Darunia (10)";
                case 0x2A: return "Ruto (Adult) (11)";
                case 0x2B: return "Saria (12)";
                case 0x2C: return "Sage? (13)";
                case 0x2F: return "Sheik (12)";
                case 0x3E: return "Navi";
                case 0x55: return "Zelda (Adult)";
                case 0x56: return "Music";
                case 0x7C: return "Music Long Fade (Action 4)";
                default: return "Actor";
            }
        }

        public override string ReadCommand()
        {
            StringBuilder sb;
            sb = new StringBuilder();

            sb.AppendLine(ToString());
            foreach (ActionEntry e in Entries)
                sb.AppendLine("   " + e.ToString());
            return sb.ToString();
        }

        protected override int GetLength()
        {
            return Entries.Count * ActionEntry.LENGTH + LENGTH;
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in Entries)
                yield return fd;
        }

        public override void AddEntry(IFrameData item)
        {
            Entries.Add((ActionEntry)item);
        }
    }
}