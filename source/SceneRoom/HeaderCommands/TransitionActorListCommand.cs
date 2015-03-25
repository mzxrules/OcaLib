using ZActor.OActors;
using ZActor.MActors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using RHelper;

namespace OcarinaPlayer.SceneRoom.Commands
{
    class TransitionActorListCommand : SceneCommand, IActorList, IBankRefAsset
    {
        Game Game { get; set; }
        public long Offset { get; set; }
        public long TransitionActorAddress { get { return Offset; } set { Offset = value; } }
        public List<TransitionActor> TransitionActorList = new List<TransitionActor>();
        public int TransitionActors { get; set; }

        public TransitionActorListCommand(Game game)
        {
            Game = game;
        }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            TransitionActors = command[1];
            if (command[4] == (byte)ORom.Bank.scene)
            {
                TransitionActorAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Initialize(BinaryReader br)
        {
            byte[] actorArray;

            actorArray = new byte[ActorRecord.SIZE];

            br.BaseStream.Position = TransitionActorAddress;
            for (int i = 0; i < TransitionActors; i++)
            {
                br.Read(actorArray, 0, 16);
                TransitionActorList.Add(TransitionActorFactory.New(actorArray));
            }
        }

        public override string Read()
        {
            string result;
            result = ReadSimple();
            foreach (TransitionActor a in TransitionActorList)
            {
                result += Environment.NewLine + a.Print();
            }
            return result;
        }
        public override string ReadSimple()
        {
            return string.Format("There are {0} transition actor(s). List starts at {1:X8}.",
                TransitionActorList.Count, TransitionActorAddress);
        }

        public List<ActorRecord> GetActors()
        {
            return TransitionActorList.Cast<ActorRecord>().ToList();
        }
    }
}