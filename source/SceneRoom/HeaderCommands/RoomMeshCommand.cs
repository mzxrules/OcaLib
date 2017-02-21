using mzxrules.Helper;
namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class RoomMeshCommand : SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int RoomMeshAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
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
