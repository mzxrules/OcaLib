using System.Collections.Generic;
using System.Linq;

namespace mzxrules.OcaLib
{
    public partial class MRom
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
                    new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.UNKNOWN, _Name = "Unknown", _Localization = MRom.Localization.UNKNOWN},

                    //new BuildInformation { _CRC = 0xEC7011B77616D72B, _Version = Build.N0, _Name = "NTSC 1.0", _Localization = Localization.NTSC },
                    //new BuildInformation { _CRC = 0xD43DA81F021E1E19, _Version = Build.N1, _Name = "NTSC 1.1", _Localization = Localization.NTSC },
                    //new BuildInformation { _CRC = 0x693BA2AEB7F14E9F, _Version = Build.N2, _Name = "NTSC 1.2", _Localization = Localization.NTSC },

                    //new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.P0, _Name = "PAL 1.0", _Localization = Localization.PAL },
                    //new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.P1, _Name = "PAL 1.1", _Localization = Localization.PAL },

                    //new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.GCNJ, _Name = "NTSC GCN", _Localization = Localization.NTSC },
                    //new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.GCNP, _Name = "NTSC PAL", _Localization = Localization.NTSC },
            
                    //new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.MQJ, _Name = "NTSC Master Quest", _Localization = Localization.NTSC },
                    //new BuildInformation { _CRC = 0x1D4136F3AF63EEA9, _Version = Build.MQP, _Name = "PAL Master Quest", _Localization = Localization.PAL },
                    //new BuildInformation { _CRC = 0x0000000000000000, _Version = Build.DBGMQ, _Name = "Debug Master Quest", _Localization = Localization.PAL },
                };
            }

            public static MRom.BuildInformation Get(Build v)
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

        private static Language[] SupportedLanguages = new Language[] { 
            Language.Japanese,
            Language.English,
            Language.German,
            Language.French, };

        public enum Build
        {
            UNKNOWN,
            CUSTOM,
            J0,
            J1,
            U0,
            P0,
            P1,
            GCJ,
            GCU,
            GCP,
            DBG, 

        }

        private static Build[] SupportedBuilds = new Build[] {
            Build.J0,
            Build.U0,


        };

        public enum Localization
        {
            UNKNOWN,
            NTSC,
            PAL
        }

        public static IEnumerable<Build> GetSupportedBuilds()
        {
            yield return Build.J0;
            yield return Build.U0; //NTSC 1.0
            //yield return Build.N1; //NTSC 1.1
            //yield return Build.N2; //NTSC 1.2

            //yield return Build.P0; //PAL 1.0 
            //yield return Build.P1; //PAL 1.1

            //yield return Build.GCNJ;
            //yield return Build.GCNP;
        }

        public static Localization GetLocalization(RomVersion v)
        {
            return BuildInformation.GetLocalization(v);

            //switch (v)
            //{
            //    case Build.N0: return Localization.NTSC; //NTSC 1.0
            //    case Build.N1: return Localization.NTSC; //NTSC 1.1
            //    case Build.N2: return Localization.NTSC; //NTSC 1.2

            //    case Build.P0: return Localization.PAL; //PAL 1.0 
            //    case Build.P1: return Localization.PAL; //PAL 1.1

            //    case Build.GCNJ: return Localization.NTSC;
            //    case Build.GCNP: return Localization.PAL;

            //    case Build.MQP: return Localization.PAL;
            //    case Build.MQJ: return Localization.NTSC;
            //    case Build.DBGMQ: return Localization.PAL;

            //    //non-official builds
            //    //case Build.DUNGRUSH: return Localization.NTSC;
            //    //case Build.DUNGRUSH2: return Localization.NTSC;
            //    default: return Localization.UNKNOWN;
            //}
        }

        public static ulong GetCrc(RomVersion v)
        {
            return BuildInformation.GetCrc(v);
        }


        public static IEnumerable<Language> GetSupportedLanguages(RomVersion b)
        {
            return GetSupportedLanguages(GetLocalization(b));
        }

        public static IEnumerable<Language> GetSupportedLanguages(Localization l)
        {
            switch (l)
            {
                case Localization.NTSC: return new Language[] { Language.Japanese, Language.English };
                case Localization.PAL: return new Language[] { Language.English, Language.German, Language.French };
                default: return null;
            }
        }

        public static bool IsBuildNintendo(RomVersion build)
        {
            if (build == Build.UNKNOWN
                || build == Build.CUSTOM)
                return false;
            return true;
        }

        public static IEnumerable<Language> GetAllSupportedLanguages()
        {
            yield return Language.Japanese;
            yield return Language.English;
            yield return Language.German;
            yield return Language.French;
        }

        public IEnumerable<Language> GetSupportedLanguages()
        {
            return GetSupportedLanguages(Version);
        }
    }
}
