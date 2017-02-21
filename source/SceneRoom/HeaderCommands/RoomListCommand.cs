using mzxrules.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    public class RoomListCommand : SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int RoomListAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public int Rooms { get; set; }
        public List<FileAddress> RoomAddresses { get; set; }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Rooms = Command.Data1;
            SegmentAddress = Command.Data2;

            if (command[4] != (byte)ORom.Bank.scene)
                throw new Exception();
        }

        public void Initialize(BinaryReader br)
        {
            RoomAddresses = new List<FileAddress>();
            br.BaseStream.Position = RoomListAddress;

            for (int i = 0; i < Rooms; i++)
            {
                var fileAddress = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());
                RoomAddresses.Add(fileAddress);
            }
        }

        public override string Read()
        {
            string result;

            result = ReadSimple();

            foreach (FileAddress address in RoomAddresses)
            {
                result += string.Format("{0}{1:X8} {2:X8}", Environment.NewLine, address.Start, address.End);
            }
            return result;
        }

        public override string ReadSimple()
        {
            return string.Format("There are {0} room(s). List starts at {1:X8}",
                RoomAddresses.Count, RoomListAddress);
        }
    }
}
