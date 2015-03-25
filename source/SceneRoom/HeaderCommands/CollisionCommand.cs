using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace OcarinaPlayer.SceneRoom.Commands
{
    class CollisionCommand : SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long CollisionHeaderAddress { get { return Offset; } set { Offset = value; } }
        public CollisionMesh Mesh;

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            if (command[4] == (byte)ORom.Bank.scene)
            {
                CollisionHeaderAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
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
