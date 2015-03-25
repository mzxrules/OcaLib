using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcarinaPlayer
{
    public partial class ORom
    {
        public class BuildInformation
        {
            public static BuildInformation[] builds;

            public Build Version { get { return _Version; } }
            public string Name { get { return _Name; } }
            public Localization Localization { get { return _Localization; } }
            public ulong CRC { get { return _CRC; } }

            Build _Version;
            string _Name;
            Localization _Localization;
            ulong _CRC;

            BuildInformation() { }

            static BuildInformation()
            {
                builds = new BuildInformation[] {
                    new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.UNKNOWN, _Name = "Unknown", _Localization = ORom.Localization.UNKNOWN},

                    new BuildInformation { _CRC = 0xEC7011B77616D72B, _Version = Build.N0, _Name = "NTSC 1.0", _Localization = Localization.NTSC },
                    new BuildInformation { _CRC = 0xD43DA81F021E1E19, _Version = Build.N1, _Name = "NTSC 1.1", _Localization = Localization.NTSC },
                    new BuildInformation { _CRC = 0x693BA2AEB7F14E9F, _Version = Build.N2, _Name = "NTSC 1.2", _Localization = Localization.NTSC },

                    new BuildInformation { _CRC = 0xB044B569373C1985, _Version = Build.P0, _Name = "PAL 1.0", _Localization = Localization.PAL },
                    new BuildInformation { _CRC = 0xB2055FBD0BAB4E0C, _Version = Build.P1, _Name = "PAL 1.1", _Localization = Localization.PAL },

                    new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.GCNJ, _Name = "NTSC GCN", _Localization = Localization.NTSC },
                    new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.GCNP, _Name = "NTSC PAL", _Localization = Localization.NTSC },
            
                    new BuildInformation { _CRC = 0xF3DD35BA4152E075, _Version = Build.MQJ, _Name = "US Master Quest", _Localization = ORom.Localization.NTSC},
                    //new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.MQJ, _Name = "NTSC Master Quest", _Localization = Localization.NTSC },
                    new BuildInformation { _CRC = 0x1D4136F3AF63EEA9, _Version = Build.MQP, _Name = "PAL Master Quest", _Localization = Localization.PAL },
                    new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.DBGMQ, _Name = "Debug Master Quest", _Localization = Localization.PAL },
                };
            }

            public static ORom.BuildInformation Get(Build v)
            {
                return builds.SingleOrDefault(x => x._Version == v);
            }
            public static Localization GetLocalization(Build v)
            {
                BuildInformation stats = builds.SingleOrDefault(x => x._Version == v);
                return (stats != null) ? stats._Localization : Localization.UNKNOWN;
            }

            internal static ulong GetCrc(Build v)
            {
                BuildInformation stats = builds.SingleOrDefault(x => x._Version == v);
                return (stats != null) ? stats._CRC : 0;
            }
        }


        internal enum Bank
        {
            unknown0 = 0,
            unknown1 = 1,
            scene = 2,
            map = 3
        }

        private static Language[] SupportedLanguages = new Language[] { 
            Rom.Language.Japanese,
            Rom.Language.English,
            Rom.Language.German,
            Rom.Language.French, };

        public enum Build
        {
            UNKNOWN,
            CUSTOM,
            N0, //NTSC 1.0
            N1, //NTSC 1.1
            N2, //NTSC 1.2
            
            P0, //PAL 1.0 
            P1, //PAL 1.1

            GCNJ,
            GCNP,

            MQJ,
            MQP,
            DBGMQ,

            //non-official builds
            DUNGRUSH,
            DUNGRUSH2,
        }

        private static Build[] SupportedBuilds = new Build[] {
            Build.N0, //NTSC 1.0
            Build.N1,  //NTSC 1.1
            Build.N2, //NTSC 1.2

            Build.P0, //PAL 1.0 
            Build.P1, //PAL 1.1

            Build.GCNJ,
            //yield return Build.GCNP;

            Build.MQP,
            //yield return Build.MQJ; 
            Build.DBGMQ,

        };

        public enum Localization
        {
            UNKNOWN,
            NTSC,
            PAL
        }

        public static IEnumerable<Build> GetSupportedBuilds()
        {
            yield return Build.N0; //NTSC 1.0
            yield return Build.N1; //NTSC 1.1
            yield return Build.N2; //NTSC 1.2

            yield return Build.P0; //PAL 1.0 
            yield return Build.P1; //PAL 1.1

            yield return Build.GCNJ;
            //yield return Build.GCNP;

            yield return Build.MQP;
            //yield return Build.MQJ; 
            yield return Build.DBGMQ;

        }

        public static Localization GetLocalization(Build v)
        {
            return BuildInformation.GetLocalization(v);

        }

        public static ulong GetCrc(Build v)
        {
            return BuildInformation.GetCrc(v);
        }


        public static IEnumerable<ORom.Language> GetSupportedLanguages(Build b)
        {
            return GetSupportedLanguages(GetLocalization(b));
        }

        public static IEnumerable<ORom.Language> GetSupportedLanguages(Localization l)
        {
            switch (l)
            {
                case Localization.NTSC: return new Language[] { Language.Japanese, Language.English };
                case Localization.PAL: return new Language[] { Language.English, Language.German, Language.French };
                default: return null;
            }
        }

        public static bool IsBuildNintendo(Build build)
        {
            if (build == Build.UNKNOWN
                || build == Build.CUSTOM
                || build == Build.DUNGRUSH
                || build == Build.DUNGRUSH2)
                return false;
            return true;
        }

        public static IEnumerable<ORom.Language> GetAllSupportedLanguages()
        {
            yield return ORom.Language.Japanese;
            yield return ORom.Language.English;
            yield return ORom.Language.German;
            yield return ORom.Language.French;
        }

        public IEnumerable<ORom.Language> GetSupportedLanguages()
        {
            return GetSupportedLanguages(Version);
        }
    }
}
