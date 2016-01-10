using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    public class RoomListCommand : SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long RoomListAddress { get { return Offset; } set { Offset = value; } }
        public int Rooms { get; set; }
        public List<FileAddress> RoomAddresses { get; set; }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Rooms = command[1];
            if (command[4] == (byte)ORom.Bank.scene)
            {
                RoomListAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Initialize(BinaryReader sr)
        {
            byte[] virtualAddress;
            
            virtualAddress = new byte[sizeof(Int32) * 2];

            sr.BaseStream.Position = RoomListAddress;
            RoomAddresses = new List<FileAddress>();
            for (int i = 0; i < Rooms; i++)
            {
                sr.Read(virtualAddress, 0, sizeof(Int32) * 2);
                RoomAddresses.Add(new FileAddress(virtualAddress));
            }
        }

        public override string Read()
        {
            string result;

            result = ReadSimple();
            foreach (FileAddress address in RoomAddresses)
            {
                result += string.Format("{0}{1:X8}", Environment.NewLine, address.Start);
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
