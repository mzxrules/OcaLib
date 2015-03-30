using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class WindSettingsCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Wind Settings: Forces Westward {0:X2}, Vertical {1:X2}, Southward {2:X2}, Strength {3:X2}",
                command[4],
                command[5],
                command[6],
                command[7]);
        }
    }
}
