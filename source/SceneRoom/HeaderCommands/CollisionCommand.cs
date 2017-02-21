using mzxrules.Helper;
using System;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class CollisionCommand : SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int CollisionHeaderAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public CollisionMesh Mesh;

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
        }

        public void Initialize(System.IO.BinaryReader br)
        {
            Mesh = new CollisionMesh(0, CollisionHeaderAddress);
            Mesh.Initialize(br);
        }
        public override string Read()
        {
            string result;
            result = ReadSimple() + Environment.NewLine;
            result += Mesh.Print();
            return result;
        }
        public override string ReadSimple()
        {
            return string.Format("Collision Header starts at {0:X8}", CollisionHeaderAddress);
        }
    }
}
