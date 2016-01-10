using mzxrules.Helper;

namespace mzxrules.ZActor.OActors
{
    class OcarinaPlaySpotActor:ActorRecord_Wrapper
    {
        SwitchFlag flag;
        byte song;
        byte type;

        public OcarinaPlaySpotActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag    = Shift.AsByte(Variable, 0x003F);
            song    = Shift.AsByte(Variable, 0x03C0);
            type    = Shift.AsByte(Variable, 0x1C00);
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
