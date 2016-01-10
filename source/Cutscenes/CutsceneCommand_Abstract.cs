using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.Cutscenes
{
    public abstract class AbstractCutsceneCommand
    {
        protected abstract IEnumerable<IFrameData> GetIFrameDataEnumerator();
        public Int32 Command { get; protected set; }
        public long Index { get; protected set; } 
        public int Length { get { return GetLength(); } }

        protected abstract int GetLength();

        public abstract string ReadCommand();

        public abstract void Save(BinaryWriter bw);

        public abstract void AddEntry(IFrameData item);

        public abstract void RemoveEntry(IFrameData item);
    }
}
