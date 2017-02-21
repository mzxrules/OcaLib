namespace mzxrules.OcaLib.SceneRoom
{
    public struct EntranceDef
    {
        public byte Room;
        public byte Position;
        public EntranceDef(byte position, byte room)
        {
            Room = room;
            Position = position;
        }
    }
}