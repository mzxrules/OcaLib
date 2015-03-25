using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace OcarinaPlayer.SceneRoom.Commands
{
    class ObjectListCommand : SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long ObjectListAddress { get { return Offset; } set { Offset = value; } }
        public int Objects { get; set; }
        public List<ushort> ObjectList = new List<ushort>();

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            Objects = command[1];
            
            if (command[4] == (byte)ORom.Bank.map)
            {
                ObjectListAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }
        public void Initialize(System.IO.BinaryReader br)
        {
            byte[] objectData = new byte[sizeof(ushort)];
            ushort o;

            br.BaseStream.Position = ObjectListAddress;
            for (int i = 0; i < Objects; i++)
            {
                br.Read(objectData, 0, sizeof(ushort));
                Endian.Convert(out o, objectData, 0);
                ObjectList.Add(o);
            }
        }

        public override string Read()
        {
            string result; //output variable

            result = ReadSimple() + Environment.NewLine;

            foreach (ushort a in ObjectList)
            {
                result += a.ToString("X4") + ", ";
            }
            return result;
        }
        public override string ReadSimple()
        {
            return String.Format("There are {0} Object(s). List starts at {1:X8}",
                Objects,
                ObjectListAddress);
        }
    }
}