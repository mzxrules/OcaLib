using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.Cutscenes
{
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

        public CutsceneCommand RootCommand { get; set; }
        public short StartFrame { get; set; }
        public short EndFrame { get; set; }

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

        public void Save(BinaryWriter bw)
        {
            //0x00
            bw.Write(Terminator);
            bw.Write(Rotation);
            bw.WriteBig(Frames);
            //0x04
            bw.WriteBig(AngleOfView);
            //0x08
            bw.WriteBig(x);
            bw.WriteBig(y);
            //0x0C
            bw.WriteBig(z);
            bw.WriteBig(d);
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
    }
}
