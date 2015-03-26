using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class SkyboxModifierCommand:SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return String.Format("Data1 {1:X2}, Skybox Modifier {0:X8}",
                Command.Data2,
                Command.Data1);
        }
    }
}
