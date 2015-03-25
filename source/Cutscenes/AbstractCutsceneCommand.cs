using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OcarinaPlayer.Cutscenes
{
    abstract class AbstractCutsceneCommand
    {
        protected abstract IEnumerable<IFrameData> GetIFrameDataEnumerator();
        public UInt32 Command { get { return command; } }
        protected uint command;
        public long Index { get { return index; } }
        protected long index;
        public int Length { get { return GetLength(); } }

        protected abstract int GetLength();

        protected abstract void Load(BinaryReader br);
        public abstract string ReadCommand();
    }
}
