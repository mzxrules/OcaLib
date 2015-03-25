using RHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OcarinaPlayer
{
    public class RomFile
    {
        public FileRecord Record { get; private set; }
        public Stream Stream { get; private set; }
        public RomVersion Version { get; private set; }

        public RomFile(FileRecord record, Stream s, RomVersion version)
        {
            Record = record;
            Stream = s;
            Version = version;
        }

        public static implicit operator Stream(RomFile file)
        {
            return file.Stream;
        }
    }
}
