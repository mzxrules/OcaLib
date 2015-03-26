using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class SpecialObjectCommand: SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return String.Format("Special Objects {0:X2} {1:X8}",
                Command.Data1,
                Command.Data2);
        }
    }
}
