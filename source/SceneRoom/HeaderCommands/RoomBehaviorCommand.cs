using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class RoomBehaviorCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            SceneWord cmd = command;
            return string.Format("Room Behavior: {0:X2} : {1:X8}",
                command.Data1,
                command.Data2);
        }
    }
}
