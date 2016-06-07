namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class RoomBehaviorCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Room Behavior: {0:X2} : {1:X8}",
                Command.Data1,
                Command.Data2);
        }
    }
}
