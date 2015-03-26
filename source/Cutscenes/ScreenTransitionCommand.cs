using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.Cutscenes
{
    class ScreenTransitionCommand : CutsceneCommand, IFrameData
    {
        const int LENGTH = 0x10;
        uint a;
        ushort transition;
        public short StartFrame { get; set; }
        public short EndFrame { get; set; }
        public CutsceneCommand RootCommand
        {
            get { return this; }
            set { throw new InvalidOperationException(); }
        }
        short endFrameD;

        public ScreenTransitionCommand(uint command, BinaryReader br)
            : base(command, br)
        {
            Load(br);
        }
        protected override int GetLength()
        {
            return LENGTH;
        }
       
        private void Load(BinaryReader br)
        {
            byte[] arr;
            short startFrame;
            short endFrame;

            arr = br.ReadBytes(12);

            Endian.Convert(out a, arr, 0);
            Endian.Convert(out transition, arr, 4);
            Endian.Convert(out startFrame, arr, 6);
            Endian.Convert(out endFrame, arr, 8);
            Endian.Convert(out endFrameD, arr, 10);

            StartFrame = startFrame;
            EndFrame = endFrame;
        }
        public override string ReadCommand()
        {
            return ToString();
        }
        public override string ToString()
        {
            StringBuilder sb;

            sb = new StringBuilder();
            sb.AppendLine(String.Format("{0:X8}: Screen Transition fx", Command));
            sb.Append(string.Format(
                "{0:X8}, Transition: {1:X4}, Start: {2:X4} End: {3:X4} End: {4:X4}",
                a,
                transition,
                StartFrame,
                EndFrame,
                endFrameD));
            return sb.ToString();
        }
        protected override IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            yield return this;
        }
    }
}
