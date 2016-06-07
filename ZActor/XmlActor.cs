using System;
using mzxrules.Helper;

namespace mzxrules.ZActor
{
    public class XmlActor : OActors.ActorRecord_Wrapper
    {
        //public static int SIZE = 0x10;
        //public ushort Actor;
        //public ushort Variable;
        public string Name = "";
        public string VariableStr = "";
        protected Vector3<short> Coords = new Vector3<short>();
        protected Vector3<ushort> Rotation = new Vector3<ushort>();
        //protected int[] objectDependencies = { };

        public XmlActor(byte[] init)
        {
            Initialize(init);
        }
        public XmlActor(byte[] init, params int[] p)
        {
            Initialize(init);
            objectDependencies = p;
        }

        private void Initialize(byte[] init)
        {
            Actor = (ushort)((init[0] << 8) + init[1]);
            Variable = (ushort)((init[14] << 8) + init[15]);

            ActorDefinition actorDef;
            if (XmlActorDictionary.TryGetActorDefinition(Actor, out actorDef))
            {
                Name = actorDef.Name;
                foreach (Tuple<ushort, string, ActorDefinitionItem.Usage> value in actorDef.GetValues(init))
                {
                    switch (value.Item3)
                    {
                        case ActorDefinitionItem.Usage.ActorNumber: break;
                        case ActorDefinitionItem.Usage.PositionX: Coords.x = (short)value.Item1; break;
                        case ActorDefinitionItem.Usage.PositionY: Coords.y = (short)value.Item1; break;
                        case ActorDefinitionItem.Usage.PositionZ: Coords.z = (short)value.Item1; break;
                        case ActorDefinitionItem.Usage.RotationX: Rotation.x = value.Item1; break;
                        case ActorDefinitionItem.Usage.RotationY: Rotation.y = value.Item1; break;
                        case ActorDefinitionItem.Usage.RotationZ: Rotation.z = value.Item1; break;
                        case ActorDefinitionItem.Usage.SwitchFlag: VariableStr += value.Item2 + ", "; break;
                        case ActorDefinitionItem.Usage.CollectFlag: goto case ActorDefinitionItem.Usage.SwitchFlag;
                        case ActorDefinitionItem.Usage.ChestFlag: VariableStr += String.Format("Chest Flag: {0:X2}, ", value.Item1); break;
                        default:
                            VariableStr += String.Format("{0}: {1:X}, ", value.Item2, value.Item1); break;
                    }
                }
            }
            else
            {
                Name = Actor.ToString("X4");
                VariableStr = "NoDef ";
            }
        }

        protected XmlActor()
        {
        }
        public override Vector3<short> GetCoords()
        {
            return Coords;
        }
        public override string Print()
        {
            return String.Format("{0}:{1} {2}{3}{4} {5}",
                Actor.ToString("X4"),
                Variable.ToString("X4"),
                (Name.Length > 0) ? Name + ", " : "",
                VariableStr,
                PrintCoord(),
                PrintRotation());
        }
        public override string PrintCommaDelimited()
        {
            string actorName;
            string variables;

            actorName = Name;
            variables = VariableStr;
            return String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                Actor.ToString("X4"),
                Variable.ToString("X4"),
                actorName.Replace(',', ';'),
                variables.Replace(',', ';'),
                Coords.x, Coords.y, Coords.z,
                Rotation.x.ToString("X4"), Rotation.y.ToString("X4"), Rotation.z.ToString("X4"));
        }

        //protected string PrintWithoutActor()
        //{
        //    return String.Format("{0}, {1} {2}",
        //          Variable.ToString("X4"),
        //          PrintCoord(),
        //          PrintRotation());
        //}
        //public string PrintCoord()
        //{
        //    return string.Format("({0}, {1}, {2})",
        //        Coords.x, Coords.y, Coords.z);
        //}
        //public string PrintRotation()
        //{
        //    return string.Format("({0}, {1}, {2})",
        //          Rotation.x.ToString("X4"),    //Degrees(rotation.x).ToString("F0"),
        //          Rotation.y.ToString("X4"),    //Degrees(rotation.y).ToString("F0"),
        //          Rotation.z.ToString("X4"));   //Degrees(rotation.z).ToString("F0"));
        //}
        //public string PrintCoordAndRotation()
        //{
        //    return PrintCoord() + " " + PrintRotation();
        //}
        //protected static float Degrees(ushort rx)
        //{
        //    return ((float)rx / (float)ushort.MaxValue) * 360.0f;
        //}
    }
}
