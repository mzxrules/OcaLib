using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class ExitListCommand : SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int ExitListAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public long EndOffset { get; set; }
        public List<ushort> ExitList = new List<ushort>();
        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
        }

        public void Initialize(System.IO.BinaryReader br)
        {
            long count;
            ushort index;
            if (EndOffset != 0)
            {
                count = (EndOffset - ExitListAddress) / 2;
                if (count > 0x20)
                    throw new ArgumentOutOfRangeException();
                br.BaseStream.Position = ExitListAddress;
                for (int i = 0; i < count; i++)
                {
                    Endian.Convert(out index, br.ReadBytes(2));
                    ExitList.Add(index);
                }
            }
        }
        public override string Read()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Exit List starts at {0:X8}.",
                ExitListAddress);
            foreach (ushort index in ExitList)
            {
                sb.AppendFormat(" {0:X4}", index);
            }
            return sb.ToString();
        }
        public override string ReadSimple()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0:X8},", ExitListAddress);
            foreach (ushort index in ExitList)
            {
                sb.AppendFormat("{0:X4},", index);
            }
            return sb.ToString();
        }
    }
}
