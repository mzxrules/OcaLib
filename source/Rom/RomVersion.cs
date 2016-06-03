using System;
using System.Runtime.Serialization;

namespace mzxrules.OcaLib
{
    /// <summary>
    /// Adapter class designed to let you pass in equivalent enumerations when requested
    /// </summary>
    [DataContract]
    public class RomVersion
    {
        [DataMember]
        public Game Game { get; private set; }
        [DataMember]
        ORom.Build OVer { get;  set; }
        [DataMember]
        MRom.Build MVer { get;  set; }

        private RomVersion(ORom.Build build)
        {
            Game = Game.OcarinaOfTime;
            OVer = build;
            MVer = MRom.Build.UNKNOWN;
        }

        private RomVersion(MRom.Build build)
        {
            Game = Game.MajorasMask;
            OVer = ORom.Build.UNKNOWN;
            MVer = build;
        }

        public static implicit operator RomVersion(ORom.Build v)
        {
            return new RomVersion(v);
        }
        public static implicit operator RomVersion(MRom.Build v)
        {
            return new RomVersion(v);
        }

        public static implicit operator ORom.Build(RomVersion v)
        {
            return v.OVer;
        }

        public static implicit operator MRom.Build(RomVersion v)
        {
            return v.MVer;
        }

        public static implicit operator Game(RomVersion v)
        {
            return v.Game;
        }

        public bool IsCustomBuild()
        {
            if (Game == Game.OcarinaOfTime)
                return OVer == ORom.Build.CUSTOM;
            else if (Game == Game.MajorasMask)
                return MVer == MRom.Build.CUSTOM;
            return false;
        }

        public string GetGroup()
        {
            if (MVer == MRom.Build.J0
                || MVer == MRom.Build.J1
                || MVer == MRom.Build.GCNJ)
                return "J";
            return null;
        }

        public override string ToString()
        {
            switch (Game)
            {
                case Game.OcarinaOfTime: return OVer.ToString();
                case Game.MajorasMask: return MVer.ToString();
                default: return base.ToString();
            }
        }

        public Type GetInternalType()
        {
            if (Game == Game.OcarinaOfTime)
                return OVer.GetType();
            else if (Game == Game.MajorasMask)
                return MVer.GetType();
            else return GetType();
                
        }
    }
}
