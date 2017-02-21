using mzxrules.Helper;
using System;
using System.Collections.Generic;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class EntranceDefinitionsCommand: SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int EntranceDefinitionsAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public long EntranceDefinitionsEndAddress { get; set; }
        public List<EntranceDef> EntranceDefinitions = new List<EntranceDef>();

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
        }

        public  void Initialize(System.IO.BinaryReader br)
        {
            int maxEntrances;
            byte position;
            byte room;

            br.BaseStream.Position = EntranceDefinitionsAddress;
            maxEntrances = (int)(EntranceDefinitionsEndAddress - EntranceDefinitionsAddress) / 2;
            for (int i = 0; i < maxEntrances; i++)
            {
                position = br.ReadByte();
                room = br.ReadByte();

                //if the second or more entrances are 0000, no worky
                if (i < 1 ||position != 0 || position != 0)
                    EntranceDefinitions.Add(new EntranceDef(position, room));
            }
        }

        public override string Read()
        {
            return ReadSimple();
        }

        public override string ReadSimple()
        {
            return string.Format("Entrance Index Definitions starts at {0:X8}",
                EntranceDefinitionsAddress);
        }
    }
}
