﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class PathListCommand : SceneCommand, IDataCommand
    {
        public SegmentAddress SegmentAddress { get; set; }
        private List<Path> Paths = new List<Path>();

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
        }
        public void Initialize(System.IO.BinaryReader br)
        {
            int nodes;
            int address;
            int loop = 0;

            br.BaseStream.Position = SegmentAddress.Offset;
            do
            {
                nodes = br.ReadSByte();
                br.BaseStream.Position += 3;
                address = br.ReadBigInt32();

                if (nodes <= 0 || loop >= 50
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

            sb.AppendLine(ToString());

            for (int i = 0; i < Paths.Count; i++)
            {
                sb.AppendLine($" {i:D2}) {Paths[i]}");
            }

            return sb.ToString();
        }
        public override string ToString()
        {
            return $"Pathway List starts at {SegmentAddress.Offset:X8}";
        }
        class Path
        {
            public int Address { get; private set; }
            public List<Vector3<short>> PathPoints = new List<Vector3<short>>();

            public Path(int nodes, int address, System.IO.BinaryReader br)
            {
                Address = address;
                br.BaseStream.Position = address;

                if (address < br.BaseStream.Length)
                {
                    for (int i = 0; i < nodes; i++)
                        PathPoints.Add(new Vector3<short>(br.ReadBigInt16(), br.ReadBigInt16(), br.ReadBigInt16()));
                }
            }

            public override string ToString()
            {
                string join = string.Join(") (",
                    PathPoints.Select(node => string.Join(", ", node.x, node.y, node.z)));
                return $"{Address:X6}  ({join})";
            }
        }
    }
}
