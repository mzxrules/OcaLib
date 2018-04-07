using mzxrules.Helper;
using System;
using System.IO;
using System.Text;

namespace mzxrules.OcaLib.Cutscenes
{
    public class ActionEntry : IFrameData
    {
        public CutsceneCommand RootCommand { get; set; }
        public short StartFrame { get; set; }
        public short EndFrame { get; set; }
        public const int LENGTH = 0x30;
        public ushort Action;
        public short d;

        UInt16 e, f;
        public Vector3<int> StartVertex = new Vector3<int>();
        public Vector3<int> EndVertex = new Vector3<int>();
        public Vector3<float> VertexNormal = new Vector3<float>();

        public ActionEntry(CutsceneCommand cmd, BinaryReader br)
        {
            RootCommand = cmd;
            Load(br);
        }

        private void Load(BinaryReader br)
        {
            byte[] arr = br.ReadBytes(LENGTH);

            /* 0x00 */ Endian.Convert(out Action, arr, 0);
            /* 0x02 */ Endian.Convert(out short startFrame, arr, 2);
            /* 0x04 */ Endian.Convert(out short endFrame, arr, 4);
            /* 0x06 */ Endian.Convert(out d, arr, 6);

            /* 0x08 */ Endian.Convert(out e, arr, 8);
            /* 0x0A */ Endian.Convert(out f, arr, 10);
            /* 0x0C */ Endian.Convert(StartVertex, arr, 12);
            /* 0x18 */ Endian.Convert(EndVertex, arr, 24);
            /* 0x24 */ Endian.Convert(VertexNormal, arr, 36);

            StartFrame = startFrame;
            EndFrame = endFrame;
        }

        public void Save(BinaryWriter bw)
        {
            bw.WriteBig(Action);
            bw.WriteBig(StartFrame);
            bw.WriteBig(EndFrame);
            //0x04
            bw.WriteBig(d);
            //0x08
            bw.WriteBig(e);
            bw.WriteBig(f);
            //0x12
            bw.WriteBig(StartVertex);
            bw.WriteBig(EndVertex);
            bw.WriteBig(VertexNormal);
        }

        public override string ToString()
        {
            return 
                $"Action: {Action:X4}, Start: {StartFrame:X4}, End: {EndFrame:X4}, {d:X4} {e:X4} {f:X4} " +
                $"Vertex Start: ({StartVertex.x}, {StartVertex.y}, {StartVertex.z}) " +
                $"End: ({EndVertex.x}, {EndVertex.y}, {EndVertex.z}) " +
                $"Normal: ({VertexNormal.x:F4}, {VertexNormal.y:F4}, {VertexNormal.z:F4})";
        }
    }
}
