using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class Rom
    {
        public string FileLocation { get; set; }
        public VFileTable Files { get; set; }
        public RomVersion Version => Files.Version;
        public bool IsCompressed { get; set; }


        public int Scenes { get; set; }
        public int Objects { get; set; }
        public int Particles { get; set; }
        public int Actors { get; set; }

        public enum Language
        {
            Japanese,
            English,
            French,
            German,
            Spanish
        }

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

        public static Rom New(string fileLocation, RomVersion version)
        {
            if (version.Game == Game.OcarinaOfTime)
                return new ORom(fileLocation, version);
            else if (version.Game == Game.MajorasMask)
                return new MRom(fileLocation, version);
            else
                return null;
        }
    }
}