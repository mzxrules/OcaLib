using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class JpegBackgroundCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return String.Format("JPEG background camera related: {0:X2} Overworld Region:  {1:X2}",
                Command.Data1,
                (byte)Command.Data2);
        }
    }
}