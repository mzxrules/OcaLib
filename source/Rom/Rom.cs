using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public partial class Rom
    {
        public string FileLocation { get; set; }
        public VFileTable Files { get; set; }
        public RomVersion Version { get { return Files.Version; } }
        public bool IsCompressed { get; set; }

        protected Rom(string fileLocation, RomVersion version)
        {
            if (version.Game == Game.OcarinaOfTime)
                Files = new OFileTable(fileLocation, version);
            else
                Files = new MFileTable(fileLocation, version);
            IsCompressed = false;
            foreach (FileRecord record in Files)
            {
                IsCompressed = record.IsCompressed;
                if (IsCompressed)
                    break;
            }
        }

        Rom(string fileLocation)
        {
            FileLocation = fileLocation;
        }

        public static Rom New(string fileLocation, RomVersion version)
        {
            if (version.Game == Game.OcarinaOfTime)
                return new ORom(fileLocation, version);
            else if (version.Game == Game.MajorasMask)
                return new MRom(fileLocation, version);
            else
                return null;
        }

        public int SceneCount { get; set; }
    }
}