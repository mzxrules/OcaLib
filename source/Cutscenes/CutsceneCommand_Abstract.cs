using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mzxrules.OcaLib.Cutscenes
{
    abstract class AbstractCutsceneCommand
    {
        protected abstract IEnumerable<IFrameData> GetIFrameDataEnumerator();
        public UInt32 Command { get; protected set; }
        public long Index { get; protected set; } 
        public int Length { get { return GetLength(); } }

        protected abstract int GetLength();

        public abstract string ReadCommand();

        public abstract void Save(BinaryWriter bw);
    }
}
