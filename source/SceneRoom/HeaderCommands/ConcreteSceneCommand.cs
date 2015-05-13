﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.SceneRoom
{
    public class SceneCommand : AbstractSceneCommand
    {
        public override string Read()
        {
            return string.Format("{0:X2} {1:X2} {2:X8}", this.ID, Command.Data1, Command.Data2);
            //throw new NotImplementedException();
        }

        public override string ReadSimple()
        {
            return Read();
            //throw new NotImplementedException();
        }

        public override void SetCommand(SceneWord command)
        {
            Command = command;
        }
    }
}
