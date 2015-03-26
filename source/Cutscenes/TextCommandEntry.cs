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
                (text != 0xFFFF) ? "Text " + text.ToString("X4") : "No Text",
                StartFrame,
                EndFrame,
                a,
                b,
                c);
        }
    }
}
