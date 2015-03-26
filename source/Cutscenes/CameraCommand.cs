using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RHelper;

namespace mzxrules.OcaLib.Cutscenes
{
    class CameraCommand : CutsceneCommand
    {
        CameraCommandParams p;
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

            p = new CameraCommandParams(this, br);
            startFrame = p.StartFrame;

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

            return String.Format("{0:X8}: {3} Camera {2} , {1}", Command, p,
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

            public override string ToString()
            {
                return String.Format("{0:X4} Start: {1:X4} End: {2:X4} {3:X4}",
                    w,
                    StartFrame,
                    EndFrame,
                    z);
            }
        }

        class CameraCommandEntry : IFrameData
        {
            public const int LENGTH = 0x10;

            public byte Terminator;
            sbyte Rotation;
            public ushort Frames;

            public float AngleOfView;

            public short x;
            public short y;
            public short z;

            public ushort d;

            public bool IsLastEntry { get { return (Terminator == 0xFF); } }

            public void Load(CutsceneCommand cmd, short startFrame, BinaryReader br)
            {
                byte[] arr;
                byte[] arr2 = new byte[4];
                arr = br.ReadBytes(sizeof(uint) * 4);
                Terminator = arr[0];
                Rotation = (sbyte)arr[1];
                Endian.Convert(out Frames, arr, 2);

                Array.Copy(arr, 4, arr2, 0, 4);
                Endian.ReverseBytes(ref arr2, 4);
                AngleOfView = BitConverter.ToSingle(arr2, 0);

                Endian.Convert(out x, arr, 8);
                Endian.Convert(out y, arr, 10);

                Endian.Convert(out z, arr, 12);
                Endian.Convert(out d, arr, 14);

                //IFrameData
                StartFrame = startFrame;
                EndFrame = (short)(startFrame + Frames);
                RootCommand = cmd;
            }

            public override string ToString()
            {
                return String.Format("{0:X2} Frames: {1:X4} Roll: {7:F2}, View Angle: {2:F4} ({3}, {4}, {5}) {6:X4}",
                    Terminator,// == 0x00 || Terminator == 0xFF) ? "" : Terminator.ToString("X2") + " ",
                    Frames,
                    AngleOfView,
                    x,
                    y,
                    z,
                    d,
                    (float)Rotation * 180 / 128);
            }

            public CutsceneCommand RootCommand
            {
                get;
                set;
            }

            public short StartFrame
            {
                get;
                set;
            }

            public short EndFrame
            {
                get;
                set;
            }
        }
    }
}
