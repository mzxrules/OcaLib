using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcarinaPlayer.SceneRoom.Commands
{
    class SoundSettingsCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Sound Settings: Reverb {0}, Playback option {1}, Song {2:X2}.",
                Command.Data1,
                (command[6] == 0x13) ? "Always Playing" : command[6].ToString("X2"),
                command[7]);
        }
    }
}