using System;
using mzxrules.OcaLib.Helper;
//OCA LIB **********************************
namespace mzxrules.OcaLib.Actor
{
    public class ActorRecord
    {
        public static int SIZE = 0x10;
        public ushort Actor;
        public ushort Variable;
        protected Vector3<short> coords = new Vector3<short>();
        protected Vector3<ushort> rotation = new Vector3<ushort>();
        protected int[] objectDependencies = { };
        public ActorRecord(byte[] record, params int[] p)
        {
            Endian.Convert(out Actor, record, 0);

            Endian.Convert(out coords.x, record, 2);
            Endian.Convert(out coords.y, record, 4);
            Endian.Convert(out coords.z, record, 6);

            Endian.Convert(out rotation.x, record, 8);
            Endian.Convert(out rotation.y, record, 10);
            Endian.Convert(out rotation.z, record, 12);

            Endian.Convert(out Variable, record, 14);
            objectDependencies = p;
        }
        protected ActorRecord()
        {
        }
        public virtual Vector3<short> GetCoords()
        {
            return coords;
        }
        public virtual string Print()
        {
            string actorName;
            string variables;

            actorName = GetActorName();
            variables = GetVariable();
            return string.Format("{0:X4}:{1:X4} {2}{3}{4} {5}",
                Actor,
                Variable,
                (actorName.Length > 0) ? actorName + ", " : "",
                (variables.Length > 0) ? variables + ", " : "",
                PrintCoord(),
                PrintRotation());
        }
        public virtual string PrintCommaDelimited()
        {
            string actorName;
            string variables;

            actorName = GetActorName();
            variables = GetVariable();
            return string.Format("{0:X4},{1:X4},{2},{3},{4},{5},{6},{7:X4},{8:X4},{9:X4}",
                Actor,
                Variable,
                actorName.Replace(',', ';'),
                variables.Replace(',', ';'),
                coords.x, coords.y, coords.z,
                rotation.x, rotation.y, rotation.z);
        }
        protected virtual string GetVariable()
        {
            return "";
        }
        protected virtual string GetActorName()
        {
            return "???";
        }
        protected string PrintWithoutActor()
        {
            return string.Format("{0:X4}, {1} {2}",
                  Variable,
                  PrintCoord(),
                  PrintRotation());
        }
        public virtual string PrintCoord()
        {
            return string.Format("({0}, {1}, {2})",
                coords.x, coords.y, coords.z);
        }
        public virtual string PrintRotation()
        {
            return string.Format("({0:X4}, {1:X4}, {2:X4})",
                  rotation.x,
                  rotation.y,
                  rotation.z);
        }
        public string PrintCoordAndRotation()
        {
            return PrintCoord() + " " + PrintRotation();
        }
        protected static float Degrees(ushort v)
        {
            return ((float)v / ushort.MaxValue) * 360.0f;
        }
    }
}
