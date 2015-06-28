using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    class CameraCommand : CutsceneCommand
    {
        CameraCommandParams Params;
        List<CameraCommandEntry> entries = new List<CameraCommandEntry>();

        public CameraCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }

        protected override int GetLength()
        {
            return (4 + CameraCommandParams.LENGTH + (CameraCommandEntry.LENGTH * entries.Count));
        }

        private void Load(BinaryReader br)
        {
            CameraCommandEntry entry;
            short startFrame;

            Params = new CameraCommandParams(this, br);
            startFrame = Params.StartFrame;

            do
            {
                entry = new CameraCommandEntry();
                entry.Load(this, startFrame, br);
                startFrame += (short)entry.Frames;

                entries.Add(entry);
            }
            while (!entry.IsLastEntry);
        }

        public override string ToString()
        {
            string commandType;
            string relativity = string.Empty;
            switch (Command)
            {
                case 01: relativity = "Static"; commandType = "Positions"; break;
                case 02: relativity = "Static"; commandType = "Focus Points"; break;
                case 05: relativity = "Player Relative"; commandType = "Positions"; break;
                case 06: relativity = "Player Relative"; commandType = "Focus Point"; break;
                default: commandType = "Unknown Command"; break;
            }

            return String.Format("{0:X8}: {3} Camera {2} , {1}", Command, Params,
                commandType, relativity);
        }

        public override string ReadCommand()
        {
            StringBuilder result;

            result = new StringBuilder();
            result.AppendLine(ToString());
            foreach (CameraCommandEntry e in entries)
                result.AppendLine("   " + e.ToString());
            return result.ToString();
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData e in entries)
                yield return e;
        }

        public override void Save(BinaryWriter bw)
        {
            //Head
            Params.Save(bw);
            foreach (CameraCommandEntry item in entries)
                item.Save(bw);
        }

        class CameraCommandParams : IFrameData
        {
            public const int LENGTH = 8;
            public CutsceneCommand RootCommand { get; set; }
            public short StartFrame { get; set; }
            public short EndFrame { get; set; }

            public ushort w;
            public ushort z;

            public CameraCommandParams(CutsceneCommand cmd, BinaryReader br)
            {
                RootCommand = cmd;
                w = br.ReadBigUInt16();
                StartFrame = br.ReadBigInt16();
                EndFrame = br.ReadBigInt16();
                z = br.ReadBigUInt16();
            }

            public void Save(BinaryWriter bw)
            {
                bw.WriteBig(RootCommand.Command);
                bw.WriteBig(w);
                bw.WriteBig(StartFrame);
                bw.WriteBig(EndFrame);
                bw.WriteBig(z);
            }

            public override string ToString()
            {
                return String.Format("{0:X4} Start: {1:X4} End: {2:X4} {3:X4}",
                    w,
                    StartFrame,
                    EndFrame,
                    z);
            }
        }
    }
}
