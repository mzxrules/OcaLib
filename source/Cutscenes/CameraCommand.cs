using mzxrules.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mzxrules.OcaLib.Cutscenes
{
    public class CameraCommand : CutsceneCommand, IFrameCollection, IFrameData
    {
        public CutsceneCommand RootCommand { get { return this; } set { throw new InvalidOperationException(); } }
        public CameraCommand.Type CommandType { get { return GetCameraCommandType(); } }

        const int LENGTH = 12;
        public short StartFrame { get; set; }
        public short EndFrame { get; set; }

        public IEnumerable<IFrameData> IFrameDataEnum => throw new NotImplementedException();

        public ushort UnknownA;
        public ushort zero;

        public List<CameraCommandEntry> Entries = new List<CameraCommandEntry>();

        public CameraCommand(int command, BinaryReader br, long index)
            : base(command, br, index)
        {
            Load(br);
        }

        public CameraCommand(int command)
        {
            Load(command);
        }

        public CameraCommand(int command, short startFrame, short endFrame)
        {
            Load(command, startFrame, endFrame);
        }

        public void AddEntry(IFrameData d)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntry(IFrameData i)
        {
            CameraCommandEntry entry = (CameraCommandEntry)i;
            Entries.Remove(entry);
        }

        private void Load(int command, short startFrame = 0, short endFrame = 0)
        {
            Command = command;
            UnknownA = 1;
            StartFrame = startFrame;
            EndFrame = endFrame;
            zero = 0;
        }

        private void Load(BinaryReader br)
        {
            CameraCommandEntry entry;
            short startFrame;

            UnknownA = br.ReadBigUInt16();
            StartFrame = br.ReadBigInt16();
            EndFrame = br.ReadBigInt16();
            zero = br.ReadBigUInt16();

            startFrame = StartFrame;

            do
            {
                entry = new CameraCommandEntry();
                entry.Load(this, startFrame, br);
                startFrame += (short)entry.Frames;

                Entries.Add(entry);
            }
            while (!entry.IsLastEntry);
        }


        private CameraCommand.Type GetCameraCommandType()
        {
            switch (Command)
            {
                case 01: return CameraCommand.Type.Position;
                case 05: return CameraCommand.Type.Position;
                case 02: return CameraCommand.Type.FocusPoint;
                case 06: return CameraCommand.Type.FocusPoint;
                default: return CameraCommand.Type.Invalid;
            }
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

            return String.Format("{0:X8}: {3} Camera {2}, {1}", Command, ParamsToString(),
                commandType, relativity);
        }

        public string ParamsToString()
        {
            return String.Format("{0:X4} Start: {1:X4} End: {2:X4} {3:X4}",
                UnknownA,
                StartFrame,
                EndFrame,
                zero);
        }

        public override string ReadCommand()
        {
            StringBuilder result;

            result = new StringBuilder();
            result.AppendLine(ToString());
            foreach (CameraCommandEntry e in Entries)
                result.AppendLine($"   {e}");
            return result.ToString();
        }

        protected override int GetLength()
        {
            return (LENGTH + (CameraCommandEntry.LENGTH * Entries.Count));
        }

        public IEnumerable<IFrameData> GetIFrameDataEnumerator()
        {
            yield return this;
            foreach (IFrameData e in Entries)
                yield return e;
        }

        public override void Save(BinaryWriter bw)
        {
            //Head
            bw.WriteBig(Command);
            bw.WriteBig(UnknownA);
            bw.WriteBig(StartFrame);
            bw.WriteBig(EndFrame);
            bw.WriteBig(zero);
            foreach (CameraCommandEntry item in Entries)
                item.Save(bw);
        }

        public enum Type
        {
            FocusPoint,
            Position,
            Invalid
        }
    }
}