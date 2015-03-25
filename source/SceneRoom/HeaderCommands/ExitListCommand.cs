using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace OcarinaPlayer.SceneRoom.Commands
{
    class ExitListCommand : SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long ExitListAddress { get { return Offset; } set { Offset = value; } }
        public long EndOffset { get; set; }
        public List<ushort> ExitList = new List<ushort>();
        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            if (command[4] == (byte)ORom.Bank.scene)
            {
                ExitListAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
                throw new Exception();
        }

        public void Initialize(System.IO.BinaryReader br)
        {
            long count;
            ushort index;
            if (EndOffset != 0)
            {
                count = (EndOffset - Offset) / 2;
                if (count > 0x20)
                    throw new ArgumentOutOfRangeException();
                br.BaseStream.Position = Offset;
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
