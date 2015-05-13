using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class SkyboxSettingsCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return String.Format("Skybox {0}, Cast: {1}, Fog? {2}",
                Command[4],
                (Command[5] == 1) ? "Cloudy" : "Sunny",
                (Command[6] > 0) ? "Yes" : "No");
        }
    }
}