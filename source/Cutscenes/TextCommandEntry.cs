using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.Cutscenes
{
    class TextCommandEntry : IFrameData
    {
        public const int LENGTH = 12;
        public ushort TextId;
        public CutsceneCommand RootCommand { get; set; }
        public short StartFrame { get; set; }
        public short EndFrame { get; set; }
        public ushort Option, TextIdChoiceA, TextIdChoiceB;
        internal void Load(CutsceneCommand cmd, BinaryReader br)
        {
            short startFrame;
            short endFrame;
            byte[] arr;
            arr = br.ReadBytes(sizeof(short) * 6);

            Endian.Convert(out TextId, arr, 0);
            Endian.Convert(out startFrame, arr, 2);
            Endian.Convert(out endFrame, arr, 4);
            Endian.Convert(out Option, arr, 6);
            Endian.Convert(out TextIdChoiceA, arr, 8);
            Endian.Convert(out TextIdChoiceB, arr, 10);

            RootCommand = cmd;
            StartFrame = startFrame;
            EndFrame = endFrame;
        }
        public void Save(BinaryWriter bw)
        {
            bw.WriteBig(TextId);
            bw.WriteBig(StartFrame);
            bw.WriteBig(EndFrame);
            bw.WriteBig(Option);
            bw.WriteBig(TextIdChoiceA);
            bw.WriteBig(TextIdChoiceB);
        }
        public override string ToString()
        {
            return String.Format("{0}, Start: {1:X4}, End: {2:X4}, Option: {3:X4}  {4:X4} {5:X4}",
                (TextId != 0xFFFF) ? "Text " + TextId.ToString("X4") : "No Text",
                StartFrame,
                EndFrame,
                Option,
                TextIdChoiceA,
                TextIdChoiceB);
        }
    }
}
