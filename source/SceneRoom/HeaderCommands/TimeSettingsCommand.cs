using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class TimeSettingsCommand : SceneCommand
    {
        public override string ReadSimple()
        {
            return Read();
        }
        public override string Read()
        {
            return string.Format("Time Settings: {0:X2}{1:X2} {2:X2}",
                command[4],
                command[5],
                command[6]);
        }
    }
}
