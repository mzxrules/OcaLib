namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class RoomBehaviorCommand : SceneCommand
    {
        public override string Read()
        {
            return $"Room Behavior: ? {Command.Data1:X2}, Show invisible actors? {(Command.Data2 & 0xFF00) > 0}, "
                + $"Idle Animation {(Command.Data2 & 0xFF):X2}";
        }
        public override string ReadSimple()
        {
            return string.Format("Room Behavior: {0:X2} : {1:X8}",
                Command.Data1,
                Command.Data2);
        }
    }
}
