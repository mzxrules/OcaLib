using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom
{
    public abstract class AbstractSceneCommand
    {
        public SceneWord Command { get { return command; } set { command = value; } }
        protected SceneWord command;
        public int ID { get { return command.Code; } }

        public abstract string Read();
        public abstract string ReadSimple();
        public abstract void SetCommand(SceneWord command);
    }
}
