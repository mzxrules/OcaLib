using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class EntranceDefinitionsCommand: SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long EntranceDefinitionsAddress { get { return Offset; } set { Offset = value; } }
        public long EntranceDefinitionsEndAddress { get; set; }
        public List<EntranceDef> EntranceDefinitions = new List<EntranceDef>();

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            if (command[4] == (byte)ORom.Bank.scene)
            {
                EntranceDefinitionsAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }

        public  void Initialize(System.IO.BinaryReader br)
        {
            int maxEntrances;
            byte[] bShort = new byte[2];

            br.BaseStream.Position = EntranceDefinitionsAddress;
            maxEntrances = (int)(EntranceDefinitionsEndAddress - EntranceDefinitionsAddress) / 2;
            for (int i = 0; i < maxEntrances; i++)
            {
                br.Read(bShort, 0, 2);
                //if the second or more entrances are 0000, no worky
                if (i < 1 || bShort[0] != 0 || bShort[1] != 0)
                    EntranceDefinitions.Add(new EntranceDef(bShort[0], bShort[1]));
            }
        }

        public override string Read()
        {
            return ReadSimple();
        }

        public override string ReadSimple()
        {
            return string.Format("Entrance Index Definitions starts at {0}",
                EntranceDefinitionsAddress.ToString("X8"));
        }
    }
}
