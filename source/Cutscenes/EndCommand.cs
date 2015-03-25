using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OcarinaPlayer.Cutscenes
{
    class EndCommand : CutsceneCommand
    {
        const int LENGTH = 8;
        public EndCommand(uint command, BinaryReader br)
            :base(command, br)
        {
        }
        protected override void Load(BinaryReader br)
        {
            br.ReadInt32();
        }
        public override string ToString()
        {
            return String.Format("{0:X8}: End Cutscene {1:X4}", command, index + Length);
        }
        public override string ReadCommand()
        {
            return ToString();
        }
        protected override int GetLength()
        {
            return LENGTH;
        }
    }
}
