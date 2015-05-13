using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class OcarinaPlaySpotActor:ActorRecord
    {
        SwitchFlag flag;
        byte song;
        byte type;

        public OcarinaPlaySpotActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag    = Pack.AsByte(Variable, 0x003F);
            song    = Pack.AsByte(Variable, 0x03C0);
            type    = Pack.AsByte(Variable, 0x1C00);
        }
        protected override string GetActorName()
        {
            return "Music Staff Spot";
        }
        protected override string GetVariable()
        {
            return string.Format("Type {0}, Song {1}, {2}",
                type, 
                song,
                flag);
        }
    }
}
