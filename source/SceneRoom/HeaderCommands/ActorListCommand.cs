using System;
using System.Collections.Generic;
using System.IO;
using mzxrules.Helper;
using mzxrules.OcaLib.Actor;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class ActorListCommand : SceneCommand, IActorList, ISegmentAddressAsset
    {
        Game Game { get; set; }
        public SegmentAddress SegmentAddress { get; set; }
        public int ActorListAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public int Actors { get; set; }
        public List<ActorRecord> ActorList = new List<ActorRecord>();
        private delegate ActorRecord GetActorRecord(byte[] data);
        GetActorRecord NewActor;

        public ActorListCommand(Game game)
        {
            Game = game;

            if (Game == Game.OcarinaOfTime)
                NewActor = ActorFactory.OcarinaActors;
            else if (Game == Game.MajorasMask)
                NewActor = ActorFactory.MaskActors;
        }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Actors = command.Data1;
            SegmentAddress = command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.map
                && SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
        }

        public override string Read()
        {
            string result;

            result = ReadSimple();
            foreach (ActorRecord a in ActorList)
            {
                result += Environment.NewLine + a.Print();
            }
            return result;
        }
        public override string ReadSimple()
        {
            return string.Format("There are {0} actor(s). List starts at {1:X8}",
                Actors,
                ActorListAddress);
        }

        internal List<ActorRecord> GetActorById(int id)
        {
            List<ActorRecord> result = new List<ActorRecord>();
            if (id != 0xFFFF)
            {
                foreach (ActorRecord actor in ActorList)
                {
                    if (actor.Actor == id)
                        result.Add(actor);
                }
            }
            else
            {
                result.AddRange(ActorList);
            }
            return result;
        }
        public void Initialize(BinaryReader br)
        {
            byte[] actorArray = new byte[ActorRecord.SIZE];

            br.BaseStream.Position = ActorListAddress;
            for (int i = 0; i < Actors; i++)
            {
                br.Read(actorArray, 0, ActorRecord.SIZE);
                ActorList.Add(NewActor(actorArray));
            }
        }
        public List<ActorRecord> GetActors()
        {
            return ActorList;
        }
    }
}
