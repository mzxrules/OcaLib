using mzxrules.Helper;

namespace mzxrules.ZActor.OActors
{
    class HugeStoneElevatorActor:ActorRecord_Wrapper 
    {
        byte speed;
        byte height;
        byte size;
        public HugeStoneElevatorActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            height  = Shift.AsByte(Variable, 0xF000);
            speed   = Shift.AsByte(Variable, 0x0F00);
            size    = Shift.AsByte(Variable, 0x0001);
        }
        protected override string GetActorName()
        {
            return "Huge Stone Elevator";
        }
        protected override string GetVariable()
        {
            return string.Format("{2}, Height: {0}, Speed: {1:X2}",
                (height * 80),
                speed,
                (size == 0) ? "Small" : "Large");
        }
    }
}
