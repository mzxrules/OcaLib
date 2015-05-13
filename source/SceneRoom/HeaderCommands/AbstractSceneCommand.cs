using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom
{
    public abstract class AbstractSceneCommand
    {
        public SceneWord Command { get; protected set; }
        public int ID { get { return Command.Code; } }

        public abstract string Read();
        public abstract string ReadSimple();
        public abstract void SetCommand(SceneWord command);
    }
}
