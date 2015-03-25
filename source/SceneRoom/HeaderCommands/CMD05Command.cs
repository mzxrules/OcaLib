using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace OcarinaPlayer.SceneRoom.Commands
{
    class CMD05Command : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Gerudo Command: {0:X2}{1:X2}0000 {2:X8}",
                command.Code,
                command.Data1,
                command.Data2);
                //Endian.ConvertUInt32(command, 0).ToString("X8"),
                //Endian.ConvertUInt32(command, 4).ToString("X8"));
        }
    }
}
