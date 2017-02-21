using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using mzxrules.Helper;
using mzxrules.OcaLib.Actor;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class TransitionActorListCommand : SceneCommand, IActorList, ISegmentAddressAsset
    {
        Game Game { get; set; }
        public SegmentAddress SegmentAddress { get; set; }
        public int TransitionActorAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public List<TransitionActor> TransitionActorList = new List<TransitionActor>();
        public int TransitionActors { get; set; }

        public TransitionActorListCommand(Game game)
        {
            Game = game;
        }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            TransitionActors =  Command.Data1;
            SegmentAddress = Command.Data2;
            if (command[4] != (byte)ORom.Bank.scene)
                throw new Exception();
        }

        public void Initialize(BinaryReader br)
        {
            byte[] actorArray;

            actorArray = new byte[ActorRecord.SIZE];

            br.BaseStream.Position = TransitionActorAddress;
            
            var readRemaining = br.BaseStream.Length - br.BaseStream.Position;
            var maxLoops = readRemaining / ActorRecord.SIZE;

            var loop = (maxLoops > TransitionActors) ? TransitionActors : maxLoops;
            
            for (int i = 0; i < loop; i++)
            {
                br.Read(actorArray, 0, 16);
                TransitionActorList.Add(ActorFactory.OcarinaTransitionActors(actorArray));
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