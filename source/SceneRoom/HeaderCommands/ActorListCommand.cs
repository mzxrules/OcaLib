using ZActor.OActors;
using ZActor.MActors;
using System;
using System.Collections.Generic;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class ActorListCommand: SceneCommand, IActorList, IBankRefAsset
    {
        Game Game { get; set; }
        public long Offset { get; set; }
        public long ActorListAddress { get { return Offset; } set { Offset = value; } }
        public int Actors { get; set; }
        public List<ActorRecord> ActorList = new List<ActorRecord>();
        private delegate ActorRecord GetActorRecord(byte[] data);
        GetActorRecord NewActor;

        public ActorListCommand(Game game)
        {
            Game = game;

            if (Game == Game.OcarinaOfTime)
                NewActor = ActorFactory.NewActor;//ActorList.Add(ActorFactory.NewActor(actorArray));
            if (Game == mzxrules.OcaLib.Game.MajorasMask)
                NewActor = MActorFactory.NewActor;//ActorList.Add(MActorFactory.NewActor(actorArray));
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
            return String.Format("There are {0} actor(s). List starts at {1:X8}",
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
        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Actors = command.Data1;
            if (command[4] == (Byte)ORom.Bank.map || command[4] == (Byte)ORom.Bank.scene)
            {
                ActorListAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF); 
            }
            else
            {
                throw new Exception();
            }
        }
        public void Initialize(BinaryReader br)
        {
            byte[] actorArray = new byte[ActorRecord.SIZE];

            br.BaseStream.Position = ActorListAddress;
            for (int i = 0; i < Actors; i++)
            {
                br.Read(actorArray, 0, ActorRecord.SIZE);
                ActorList.Add(NewActor(actorArray));
                //if (Game == Game.OcarinaOfTime)
                //    ActorList.Add(ActorFactory.NewActor(actorArray));
                //if (Game == OcarinaPlayer.Game.MajorasMask)
                //    ActorList.Add(MActorFactory.NewActor(actorArray));
                //ActorList.Add(new XmlActor(actorArray));
            }
        }
        public List<ActorRecord> GetActors()
        {
            return ActorList;
        }
    }
}
