using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class LizafosActor:ActorRecord_Wrapper
    {
        byte type;
        SwitchFlag flags;
        public LizafosActor(short[] record, params int[] p):base(record)
        {
            objectDependencies = p;
            type = (byte)Variable;
            flags = (byte)(Variable >> 8);
        }
        protected override string GetActorName()
        {
            //type 0 = Lizafos #1 miniboss
            //type 1 = Lizafos #2 miniboss
            return "Lizafos";
        }
        protected override string GetVariable()
        {
            return string.Format("Spawn on entering door bound to {0}",
                flags.ToString());
        }
    }
}
