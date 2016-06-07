namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class RoomMeshCommand:SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long RoomMeshAddress { get { return Offset; } set { Offset = value ;} }
        public void Initialize(System.IO.BinaryReader br)
        {

        }
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Room Mesh: {0:X8}",
                Command.Data2);
        }
    }
}
