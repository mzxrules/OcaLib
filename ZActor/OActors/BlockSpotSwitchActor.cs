using mzxrules.OcaLib.Actor;

namespace mzxrules.ZActor.OActors
{
    class BlockSpotSwitchActor:ActorRecord_Wrapper
    {
        SwitchFlag flag;
        public BlockSpotSwitchActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = (byte)(Variable);
        }
        protected override string GetActorName()
        {
            return "Block Spot Switch";
        }
        protected override string GetVariable()
        {
            return flag.ToString();
        }
    }
}
