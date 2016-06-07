using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class PathListCommand : SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long PathListAddress { get { return Offset; } set { Offset = value; } }
        private List<Path> Paths = new List<Path>();

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            if (command[4] == (byte)ORom.Bank.scene)
            {
                PathListAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }
        public void Initialize(System.IO.BinaryReader br)
        {
            int nodes;
            int address;
            int loop = 0;

            br.BaseStream.Position = PathListAddress;
            do
            {
                nodes = br.ReadSByte();
                br.BaseStream.Position += 3;
                address = br.ReadBigInt32();

                if (nodes <= 0
                    || loop >= 50
                    ||
                    ((address >> 16) & 0xFFC0) //assuming an address range of 0200 0000 to 022F FFFF, quite larger than expected
                    != (((int)ORom.Bank.scene) << 8)) //
                    break;

                var t = br.BaseStream.Position;
                Paths.Add(new Path(nodes, address & 0xFFFFFF, br));
                br.BaseStream.Position = t;

                loop++;
            }
            while (true);
        }

        public override string Read()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(ReadSimple());

            for (int i = 0; i < Paths.Count; i++)
            {
                sb.AppendLine(String.Format(" {0:D2}) {1}", i, Paths[i]));
            }

            return sb.ToString();
        }
        public override string ReadSimple()
        {
            return string.Format("Pathway List starts at {0:X8}", PathListAddress);
        }
        class Path
        {
            public int Address { get; private set; }
            public List<Vector3<short>> PathPoints = new List<Vector3<short>>();

            public Path(int nodes, int address, System.IO.BinaryReader br)
            {
                Address = address;
                br.BaseStream.Position = address;
                for (int i = 0; i < nodes; i++)
                    PathPoints.Add(new Vector3<short>(br.ReadBigInt16(), br.ReadBigInt16(), br.ReadBigInt16()));
            }

            public override string ToString()
            {
                return string.Format("{0:X6}  ({1})", Address,
                    string.Join(") (",
                    PathPoints.Select(node => string.Join(", ", node.x, node.y, node.z))));
            }
        }
    }
}
