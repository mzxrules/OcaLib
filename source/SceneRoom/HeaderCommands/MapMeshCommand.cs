using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class MapMeshCommand:SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long MapMeshAddress { get { return Offset; } set { Offset = value ;} }
        public void Initialize(System.IO.BinaryReader br)
        {

        }
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return String.Format("Map Mesh: {0:X8}",
                Command.Data2);
        }
    }
}
