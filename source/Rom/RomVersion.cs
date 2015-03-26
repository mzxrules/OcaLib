using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class RomVersion
    {
        public Game Game { get; private set; }
        ORom.Build OVer { get;  set; }
        MRom.Build MVer { get;  set; }

        private RomVersion(ORom.Build build)
        {
            Game = mzxrules.OcaLib.Game.OcarinaOfTime;
            OVer = build;
            MVer = MRom.Build.UNKNOWN;
        }

        private RomVersion(MRom.Build build)
        {
            Game = mzxrules.OcaLib.Game.MajorasMask;
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

        public override string ToString()
        {
            switch (Game)
            {
                case Game.OcarinaOfTime: return this.OVer.ToString(); 
                case Game.MajorasMask: return this.MVer.ToString();
                default: return base.ToString();
            }

        }
    }
}
