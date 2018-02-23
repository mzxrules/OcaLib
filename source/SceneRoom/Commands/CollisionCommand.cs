using mzxrules.Helper;
using System;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class CollisionCommand : SceneCommand, IDataCommand
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int CollisionHeaderAddress
        {
            get { return SegmentAddress.Offset; }
            set { SegmentAddress = new SegmentAddress(SegmentAddress, value); }
        }
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
            result = ToString() + Environment.NewLine;
            result += Mesh.Print();
            return result;
        }
        public override string ToString()
        {
            return $"Collision Header starts at {CollisionHeaderAddress:X8}";
        }
    }
}
