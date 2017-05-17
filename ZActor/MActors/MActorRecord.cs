using mzxrules.Helper;
using System;

namespace mzxrules.ZActor.MActors
{
    public class MActorRecord : mzxrules.ZActor.OActors.ActorRecord_Wrapper
    {
        Vector3<bool> RotationFlags;
        protected ushort DayFlags;
        protected byte Scene_0x1B;
        
        public MActorRecord(byte[] record)
        {
            Endian.Convert(out Actor, record, 0);
            RotationFlags = new Vector3<bool>
                (
                    (Actor & 0x8000) > 0,
                    (Actor & 0x4000) > 0,
                    (Actor & 0x2000) > 0
                );
            Actor &= 0xFFF;

            Endian.Convert(out coords.x, record, 2);
            Endian.Convert(out coords.y, record, 4);
            Endian.Convert(out coords.z, record, 6);
            
            DayFlags = (ushort)((record[9] << 7) + (record[13] & 0x7F));

            rotation.x = (ushort)(record[08] * 2 + Shift.AsByte(record[09], 0x80));
            rotation.y = (ushort)(record[10] * 2 + Shift.AsByte(record[11], 0x80));
            rotation.z = (ushort)(record[12] * 2 + Shift.AsByte(record[13], 0x80));

            Scene_0x1B = Shift.AsByte(record[13], 0x7F);

            Endian.Convert(out Variable, record, 6);
        }
        protected MActorRecord()
        {
        }
        new public virtual Vector3<short> GetCoords()
        {
            return coords;
        }
        public override string Print()
        {
            string actorName;
            string variables;

            actorName = GetActorName();
            variables = GetVariable();
            return String.Format("{0:X3}:{1:X4} {2}{3}{4} {5} {6} Days: {7} 1B?: {8:X4}",
                Actor,
                Variable,
                (actorName.Length > 0) ? actorName + ", " : "",
                (variables.Length > 0) ? variables + ", " : "",
                PrintCoord(),
                PrintRotation(),
                PrintRotationVars(),
                PrintDayFlags(),
                Scene_0x1B);
        }


        private string PrintDayFlags()
        {
            return string.Format("({0} {1}{2}{3} {4})",
                (DayFlags >> 8) & 3,
                (DayFlags >> 6) & 3,
                (DayFlags >> 4) & 3,
                (DayFlags >> 2) & 3,
                (DayFlags) & 3);
        }
        override public string PrintCommaDelimited()
        {
            string actorName;
            string variables;

            actorName = GetActorName();
            variables = GetVariable();
            return String.Format("{0:X3},{1:X4},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14:X2}",
                Actor,
                Variable,
                actorName.Replace(',', ';'),
                variables.Replace(',', ';'),
                coords.x, coords.y, coords.z,
                (!RotationFlags.x) ? rotation.x.ToString() : "",
                (!RotationFlags.y) ? rotation.y.ToString() : "",
                (!RotationFlags.z) ? rotation.z.ToString() : "",
                (RotationFlags.x) ? rotation.x.ToString("X2") : "",
                (RotationFlags.y) ? rotation.y.ToString("X2") : "",
                (RotationFlags.z) ? rotation.z.ToString("X2") : "",
                PrintDayFlags(),
                Scene_0x1B);
        }
        public override string PrintCoord()
        {
            return string.Format("({0}, {1}, {2})",
                coords.x, coords.y, coords.z);
        }
        public override string PrintRotation()
        {
            return string.Format("({0}, {1}, {2})",
                (!RotationFlags.x) ? rotation.x.ToString() : "N/A",
                (!RotationFlags.y) ? rotation.y.ToString() : "N/A",
                (!RotationFlags.z) ? rotation.z.ToString() : "N/A");
        }

        private object PrintRotationVars()
        {
            return string.Format("({0}, {1}, {2})",
                (RotationFlags.x) ? rotation.x.ToString() : "N/A",
                (RotationFlags.y) ? rotation.y.ToString() : "N/A",
                (RotationFlags.z) ? rotation.z.ToString() : "N/A");
        }
    }
}
