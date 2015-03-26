using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHelper;

namespace mzxrules.OcaLib.SceneRoom
{
    public class CollisionPolygon
    {
        public static int SIZE = 0x10;
        ushort type;
        Vector3<ushort> Vertex = new Vector3<ushort>();
        Vector3<ushort> Normals = new Vector3<ushort>();
        ushort distanceFromOrigin;

        public CollisionPolygon(byte[] data)
        {
            Endian.Convert(out type, data, 0);

            Endian.Convert(out Vertex.x, data, 2);
            Endian.Convert(out Vertex.y, data, 4);
            Endian.Convert(out Vertex.z, data, 6);

            Endian.Convert(out Normals.x, data, 8);
            Endian.Convert(out Normals.y, data, 10);
            Endian.Convert(out Normals.z, data, 12);

            Endian.Convert(out distanceFromOrigin,data, 14);
        }
        public ushort GetPolygonType()
        {
            return type;
        }
    }
    public class CollisionPolygonType
    {
        public static int SIZE = 0x8; //?
        public uint p01;
        public uint p23;

    }
}
