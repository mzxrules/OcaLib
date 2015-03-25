using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcarinaPlayer.SceneRoom.Commands
{
    class SoundSettingsEchoCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return String.Format("Sound Settings: Echo {0}", command[7]);
        }
    }
}
