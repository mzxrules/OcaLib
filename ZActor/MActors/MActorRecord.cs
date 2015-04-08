using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.MActors
{
    public class MActorRecord : ZActor.OActors.ActorRecord
    {
        protected byte ActorDataA;
        protected ushort DayFlags;
        protected byte DataC;

        //protected int[] objectDependencies = { };
        public MActorRecord(byte[] record)
        {
            Endian.Convert(out Actor, record, 0);
            ActorDataA = (byte)(Actor >> 12);
            Actor &= 0xFFF;

            Endian.Convert(out coords.x, record, 2);
            Endian.Convert(out coords.y, record, 4);
            Endian.Convert(out coords.z, record, 6);

            DayFlags = (ushort)(((ushort)record[9] << 7) + record[11]);

            rotation.x = (ushort)(record[8] * 2);
            rotation.y = (ushort)(record[10] * 2);
            rotation.z = (ushort)(record[12] * 2);

            DataC = record[13];

            Endian.Convert(out Variable, record, 6);
            //objectDependencies = p;
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
            return String.Format("{6:X1}:{0:X3}:{1:X4} {2}{3}{4} {5} Days: {7} 1B?: {8:X4}",
                Actor,
                Variable,
                (actorName.Length > 0) ? actorName + ", " : "",
                (variables.Length > 0) ? variables + ", " : "",
                PrintCoord(),
                PrintRotation(),
                ActorDataA,
                PrintDayFlags(),
                DataC);
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
        new public string PrintCommaDelimited()
        {
            string actorName;
            string variables;

            actorName = GetActorName();
            variables = GetVariable();
            return String.Format("{10:X1},{0:X3},{1:X4},{2},{3},{4},{5},{6},{7},{8},{9},{11},{12:X2}",
                Actor,
                Variable,
                actorName.Replace(',', ';'),
                variables.Replace(',', ';'),
                coords.x, coords.y, coords.z,
                rotation.x, rotation.y, rotation.z,
                ActorDataA,
                PrintDayFlags(),
                DataC);
        }
        public override string PrintCoord()
        {
            return string.Format("({0}, {1}, {2})",
                coords.x, coords.y, coords.z);
        }
        public override string PrintRotation()
        {
            return string.Format("({0}, {1}, {2})",
                  rotation.x,
                  rotation.y,
                  rotation.z);
        }
    }
}
