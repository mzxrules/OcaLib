using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class PathListCommand:SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long PathListAddress { get { return Offset; } set { Offset = value; } }

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
        public  void Initialize(System.IO.BinaryReader br)
        {
        }
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Pathway List starts at {0:X8}", PathListAddress);
        }
    }
}
