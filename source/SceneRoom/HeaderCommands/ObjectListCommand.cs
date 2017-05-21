using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class ObjectListCommand : SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int ObjectListAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public int Objects { get; set; }
        public List<ushort> ObjectList = new List<ushort>();

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Objects = Command.Data1;
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.map)
                throw new Exception();
        }
        public void Initialize(System.IO.BinaryReader br)
        {
            byte[] objectData = new byte[2];

            br.BaseStream.Position = ObjectListAddress;
            for (int i = 0; i < Objects; i++)
            {
                br.Read(objectData, 0, 2);
                Endian.Convert(out ushort o, objectData, 0);
                ObjectList.Add(o);
            }
        }

        public override string Read()
        {
            string result; //output variable

            result = ReadSimple() + Environment.NewLine;

            foreach (ushort a in ObjectList)
            {
                result += $"{a:X4}, ";
            }
            return result;
        }
        public override string ReadSimple()
        {
            return $"There are {Objects} Object(s). List starts at {ObjectListAddress:X8}";
        }
    }
}