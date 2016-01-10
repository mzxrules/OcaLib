using System;

namespace mzxrules.ZActor.OActors
{
    class ClearBlockActor : ActorRecord_Wrapper
    {
        SwitchFlag flag;
        public ClearBlockActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = new SwitchFlag(Variable, 0x3F00);
        }
        protected override string GetActorName()
        {
            return "Clear Block";
        }
        protected override string GetVariable()
        {
            return String.Format("{0}",
                flag);
        }
    }
}
