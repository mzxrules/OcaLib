using mzxrules.Helper;
using System;
using System.IO;

namespace mzxrules.OcaLib.Cutscenes
{
    public class TextCommandEntry : IFrameData
    {
        public const int LENGTH = 12;
        public ushort TextId;
        public CutsceneCommand RootCommand { get; set; }
        public short StartFrame { get; set; }
        public short EndFrame { get; set; }
        public ushort Option, TextIdChoiceA, TextIdChoiceB;
        internal void Load(CutsceneCommand cmd, BinaryReader br)
        {
            /* 0x00 */ TextId = br.ReadBigUInt16();
            /* 0x02 */ StartFrame = br.ReadBigInt16();
            /* 0x04 */ EndFrame = br.ReadBigInt16();
            /* 0x06 */ Option = br.ReadBigUInt16();
            /* 0x08 */ TextIdChoiceA = br.ReadBigUInt16();
            /* 0x0A */ TextIdChoiceB = br.ReadBigUInt16();
            RootCommand = cmd;
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
                (TextId != 0xFFFF) ? $"Text {TextId:X4}" : "No Text",
                StartFrame,
                EndFrame,
                Option,
                TextIdChoiceA,
                TextIdChoiceB);
        }
    }
}
