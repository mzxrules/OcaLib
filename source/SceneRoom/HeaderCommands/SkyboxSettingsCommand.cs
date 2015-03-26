using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class SkyboxSettingsCommand:SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return String.Format("Skybox {0}, Cast: {1}, Fog? {2}",// data1 {2:X2}, data2 {3:X8}",
                Command[4],
                (Command[5] == 1)? "Cloudy": "Sunny",
                (command[6] > 0) ? "Yes" : "No");
        }
    }
}
