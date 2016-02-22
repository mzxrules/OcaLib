using mzxrules.OcaLib.Helper;
using System;

namespace mzxrules.OcaLib.Actor
{
    public class TransitionActor : ActorRecord
    {
        byte SwitchToFrontRoom;
        byte SwitchToFrontCamera;
        byte SwitchToBackRoom;
        byte SwitchToBackCamera;
        public TransitionActor(byte[] record)
        {
            SwitchToFrontRoom = record[0];
            SwitchToFrontCamera = record[1];
            SwitchToBackRoom = record[2];
            SwitchToBackCamera = record[3];

            Endian.Convert(out Actor, record, 4);
            Endian.Convert(out coords.x, record, 6);
            Endian.Convert(out coords.y, record, 8);
            Endian.Convert(out coords.z, record, 10);
            rotation.x = 0;
            Endian.Convert(out rotation.y, record, 12);
            rotation.z = 0;
            Endian.Convert(out Variable, record, 14);
        }
        protected TransitionActor()
        {
        }
        public override string Print()
        {
            string varString;

            varString = GetVariable();
            return
                String.Format("{0}, {1:X4}:{2:X4}, {3}, {4}{5}",
                PrintTransition(),
                Actor,
                Variable,
                GetActorName(),
                (varString.Length > 0) ? varString + ", " : "",
                PrintCoordAndRotation());
        }
        protected override string GetActorName()
        {
            return "Unknown TransitionActor";
        }
        protected string PrintTransition()
        {
            return String.Format("{0:D2} {1:X2} -> {2:D2} {3:X2}",
                SwitchToBackRoom,
                SwitchToBackCamera,
                SwitchToFrontRoom,
                SwitchToFrontCamera);
        }
    }
}
