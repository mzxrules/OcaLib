using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.OcaLib.Helper;
using mzxrules.ZActor.OActors;
using System.IO;


namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class PositionListCommand : SceneCommand, IActorList, IBankRefAsset
    {
        public long Offset { get; set; }
        public long PositionListAddress { get { return Offset; } set { Offset = value; } }
        public int Positions;
        public List<LinkActor> PositionList { get; set; }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Positions = command[1];

            if (command[4] == (byte)ORom.Bank.scene)
            {
                PositionListAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Initialize(BinaryReader sr)
        {
            PositionList = new List<LinkActor>();
            byte[] positionRecord = new byte[ActorRecord.SIZE];

            sr.BaseStream.Position = PositionListAddress;
            for (int i = 0; i < Positions; i++)
            {
                sr.Read(positionRecord, 0, 16);
                PositionList.Add(new LinkActor(positionRecord));
            }
        }

        public override string Read()
        {
            string result;

            result = ReadSimple();
            foreach (LinkActor p in PositionList)
            {
                result += Environment.NewLine + p.Print();
            }
            return result;
        }

        public override string ReadSimple()
        {
            return String.Format("There are {0} position(s). List starts at {1:X8}.",
                Positions,
                PositionListAddress);
        }

        public List<ActorRecord> GetActors()
        {
            return PositionList.Cast<ActorRecord>().ToList();
        }
    }
}
