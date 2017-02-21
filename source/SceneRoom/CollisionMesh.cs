using System;
using System.Collections.Generic;
using mzxrules.Helper;

namespace mzxrules.OcaLib.SceneRoom
{
    public class CollisionMesh
    {
        public const int HEADER_SIZE = 0x2C;
        const int WATER_BOX_RECORD_SIZE = 0x10;
        Vector3<short> MinCollision = new Vector3<short>();
        Vector3<short> MaxCollision = new Vector3<short>();
        List<CollisionPolygon> PolygonList = new List<CollisionPolygon>();
        List<CollisionPolygonType> PolygonTypeList = new List<CollisionPolygonType>();
        List<short> VertexList = new List<short>();

        long SceneOffset = 0;
        long HeaderOffset;

        ushort Vertices = 0;
        long VerticesOffset = 0;

        ushort Polygons = 0;
        long PolygonOffset = 0;

        int PolygonTypes = 0;
        long PolygonTypesOffset = 0;

        bool hasCameraData = false;
        long CameraDataOffset = 0;

        int WaterBoxes = 0;
        bool hasWaterBoxesData = false;
        long WaterBoxesOffset = 0;

        public CollisionMesh(long sceneBank, long headerOffset)
        {
            SceneOffset = sceneBank;
            HeaderOffset = headerOffset;
        }

        public void Initialize(System.IO.BinaryReader br)
        {
            long seekBack;
            byte[] data;

            seekBack = br.BaseStream.Position;

            //initialize header
            data = new byte[HEADER_SIZE];
            br.BaseStream.Position = HeaderOffset;
            br.Read(data, 0, HEADER_SIZE);
            InitializeHeader(data);

            //initialize polygon list
            data = new byte[CollisionPolygon.SIZE];
            br.BaseStream.Position = PolygonOffset;
            for (int i = 0; i < Polygons; i++)
            {
                br.Read(data, 0, CollisionPolygon.SIZE);
                PolygonList.Add(new CollisionPolygon(data));
            }
            data = new byte[CollisionPolygonType.SIZE];
            br.BaseStream.Position = PolygonTypesOffset;
            PolygonTypes = GetPolygonTypeListLength();

            //initialize polygon types
            for (int i = 0; i < PolygonTypes; i++)
            {
                br.Read(data,0, CollisionPolygonType.SIZE);
                PolygonTypeList.Add(new CollisionPolygonType());
                PolygonTypeList[i].p01 = Endian.ConvertUInt32(data, 0);
                PolygonTypeList[i].p23 = Endian.ConvertUInt32(data, 4);
            }

            br.BaseStream.Position = seekBack;
        }
        public string Print()
        {
            string result;

            result = String.Format("Offset to vertex array {0:X8}" + Environment.NewLine
                + "Offset to polygon array {1:X8}" + Environment.NewLine
                + "Offset to polygon type defs {2:X8}" + Environment.NewLine
                + "Offset to camera data {3:X8}" + Environment.NewLine
                + "Offset to water boxes {4:X8}" + Environment.NewLine
                + "Size in bytes {5}",
                VerticesOffset,
                PolygonOffset,
                PolygonTypesOffset,
                (hasCameraData) ? CameraDataOffset : 0,
                (hasWaterBoxesData) ? WaterBoxesOffset : 0,
                GetFileSize());

            return result;

            //result += Environment.NewLine + "Collision Polygon Types:";

            //foreach (CollisionPolygonType pt in PolygonTypeList)
            //{
            //    result += string.Format("{0}{1:X8} {2:X8}",
            //    Environment.NewLine,
            //    pt.p01,
            //    pt.p23);
            //}
            //return result;
        }

        private void InitializeHeader(byte[] data)
        {
            UInt32 temp;
            Endian.Convert(out MinCollision.x, data, 0);
            Endian.Convert(out MinCollision.y, data, 2);
            Endian.Convert(out MinCollision.z, data, 4);

            Endian.Convert(out MaxCollision.x, data, 6);
            Endian.Convert(out MaxCollision.y, data, 8);
            Endian.Convert(out MaxCollision.z, data, 10);

            Endian.Convert(out Vertices, data, 12);
            Endian.Convert(out temp, data, 16);
            VerticesOffset = SceneOffset + (temp & 0xFFFFFF);

            Endian.Convert(out Polygons,data, 20);
            Endian.Convert(out temp, data, 24);
            PolygonOffset = SceneOffset + (temp & 0xFFFFFF);
            Endian.Convert(out temp, data, 28);
            PolygonTypesOffset = SceneOffset + (temp & 0xFFFFFF);

            Endian.Convert(out temp, data, 32);
            hasCameraData = ((temp & 0xFF000000) > 0);
            CameraDataOffset = SceneOffset + (temp & 0xFFFFFF);

            Endian.Convert(out WaterBoxes, data, 36);
            Endian.Convert(out temp, data, 40);
            hasWaterBoxesData = ((temp & 0xFF000000) > 0);
            WaterBoxesOffset = SceneOffset + (temp & 0xFFFFFF); 
        }

        public int GetPolygonTypeListLength()
        {
            return (int)((PolygonOffset - PolygonTypesOffset) / CollisionPolygonType.SIZE);
        }

        public int GetFileSize()
        {
            if (hasCameraData)
                return (int)(HeaderOffset + HEADER_SIZE - CameraDataOffset);
            else
                return (int)(HeaderOffset + HEADER_SIZE - VerticesOffset);
        }
    }
}
