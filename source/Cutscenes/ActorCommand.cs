using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RHelper;

namespace mzxrules.OcaLib.Cutscenes
{
    class ActorCommand : CutsceneCommand
    {
        const int LENGTH = 8;
        uint EntryCount;
        List<ActorCommandEntry> entries = new List<ActorCommandEntry>();

        public ActorCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }
        
        private void Load(BinaryReader br)
        {
            Endian.Convert(out EntryCount, br.ReadBytes(4));

            for (int i = 0; i < EntryCount; i++)
            {
                entries.Add(new ActorCommandEntry(this, br));
            }
        }

        public override string ToString()
        {
            return String.Format("{0:X8}: Actor, Entries: {1:X8}", Command, EntryCount);
        }

        public override string ReadCommand()
        {
            StringBuilder sb;
            sb = new StringBuilder();

            sb.AppendLine(ToString());
            foreach (ActorCommandEntry e in entries)
                sb.AppendLine("   " + e.ToString());
            return sb.ToString();
        }

        protected override int GetLength()
        {
            return entries.Count * ActorCommandEntry.LENGTH + LENGTH;
        }

        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            foreach (IFrameData fd in entries)
                yield return fd;
        }

        class ActorCommandEntry : IFrameData
        {
            public CutsceneCommand RootCommand { get; set; }
            public short StartFrame { get; set; }
            public short EndFrame { get; set; }
            public const int LENGTH = 0x30;
            public ushort action;
            public short d;

            uint e;
            public Vector3<int> StartVertex = new Vector3<int>();
            public Vector3<int> EndVertex = new Vector3<int>();
            public Vector3<float> VertexNormal = new Vector3<float>();

            public ActorCommandEntry(CutsceneCommand cmd,  BinaryReader br)
            {
                Load(cmd, br);
            }

            private void Load(CutsceneCommand cmd, BinaryReader br)
            {
                byte[] arr;
                short startFrame;
                short endFrame;

                arr = br.ReadBytes(LENGTH);

                Endian.Convert(out action, arr, 0);
                Endian.Convert(out startFrame, arr, 2);
                Endian.Convert(out endFrame, arr, 4);
                Endian.Convert(out d, arr, 6);

                Endian.Convert(out e, arr, 8);
                Endian.Convert(StartVertex, arr, 12);
                Endian.Convert(EndVertex, arr, 24);
                Endian.Convert(VertexNormal, arr, 36);

                RootCommand = cmd;
                StartFrame = startFrame;
                EndFrame = endFrame;

            }

            public override string ToString()
            {
                StringBuilder sb;
                sb = new StringBuilder();

                sb.Append(
                    String.Format("Action: {0:X4}, Start: {1:X4}, End: {2:X4}, {3:X4} {4:X4} ",
                    action,
                    StartFrame,
                    EndFrame,
                    d,
                    e));

                sb.Append(String.Format(" Vertex Start: ({0}, {1}, {2}) ",
                    StartVertex.x,
                    StartVertex.y,
                    StartVertex.z));

                sb.Append(String.Format("End: ({0}, {1}, {2}) ",
                    EndVertex.x,
                    EndVertex.y,
                    EndVertex.z));

                sb.Append(String.Format("Normal: ({0:F4}, {1:F4}, {2:F4})",
                    VertexNormal.x,
                    VertexNormal.y,
                    VertexNormal.z));

                return sb.ToString();
            }
        }
    }
}
