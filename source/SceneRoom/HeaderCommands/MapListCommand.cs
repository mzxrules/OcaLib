using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace OcarinaPlayer.SceneRoom.Commands
{
    public class MapListCommand : SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long MapListAddress { get { return Offset; } set { Offset = value; } }
        public int Maps { get; set; }
        public List<FileAddress> MapAddresses { get; set; }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Maps = command[1];
            if (command[4] == (byte)ORom.Bank.scene)
            {
                MapListAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Initialize(System.IO.BinaryReader sr)
        {
            byte[] virtualAddress;
            
            virtualAddress = new byte[sizeof(Int32) * 2];

            sr.BaseStream.Position = MapListAddress;
            MapAddresses = new List<FileAddress>();
            for (int i = 0; i < Maps; i++)
            {
                sr.Read(virtualAddress, 0, sizeof(Int32) * 2);
                MapAddresses.Add(new FileAddress(virtualAddress));
            }
        }
        public override string Read()
        {
            string result;

            result = ReadSimple();
            foreach (FileAddress address in MapAddresses)
            {
                result += string.Format("{0}{1}", Environment.NewLine, address.Start.ToString("X8"));
            }
            return result;
        }
        public override string ReadSimple()
        {
            return string.Format("There are {0} map(s). List starts at {1}",
                MapAddresses.Count, MapListAddress.ToString("X8"));
        }
    }
}
