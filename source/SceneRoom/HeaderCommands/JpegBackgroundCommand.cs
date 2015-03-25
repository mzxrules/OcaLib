using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace OcarinaPlayer.SceneRoom.Commands
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
                command.Data1,
                (byte)command.Data2);
        }
    }
}