using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class GreeneryActor : ActorRecord
    {
        byte dropType;
        byte type;

        #region Types
        string[] typeDesc = new string[] {
            "Large tree",
            "Medium tree",
            "Small tree",
            "Group of trees",
            "Medium tree",
            "Medium tree, dark brown trunk, greener leaves",
            "Group of trees, dark brown trunk, yellow leaves",
            "Medium tree, dark brown trunk, yellow leaves",
            "Group of trees, dark brown trunk, greener leaves",
            "Medium tree, dark brown trunk, greener leaves",
            "Ugly tree from Kakariko Village",
            "Bush",
            "Large bush",
            "Group of bushes",
            "Bush",
            "Group of large bushes",
            "Large bush",
            "Dark bush",
            "Large dark bush",
            "Group of dark bushes",
            "Dark bush",
            "Group of large dark bushes",
            "Large dark bush",
            "Dancing dark bush (disappears after several repetitions)",
            "Undefined"
        };
        #endregion

        #region DropType
        string[] dropTypeDesc = new string[] {
            "Rand00",
            "Rand01",
            "Rand02",
            "Rand03",
            "Rand04",
            "Rand05",
            "Rand06",
            "Rand07",
            "Deku seeds",
            "Magic jars",
            "Bombs",
            "Three Hearts/Green Rupees",
            "Three Blue Rupees",
            "Rand0D",
            "Rand0E",
            "Nothing"
        };
        #endregion

        public GreeneryActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            dropType = Pack.AsByte(Variable, 0xFF00);
            type = Pack.AsByte(Variable, 0x00FF);
        }

        protected override string GetActorName()
        {
            return "Greenery";
        }

        protected override string GetVariable()
        {
            return String.Format("{0}, {1}",
                typeDesc[(type < typeDesc.Length) ? type : typeDesc.Length - 1],
                dropTypeDesc[(dropType < dropTypeDesc.Length) ? dropType : dropTypeDesc.Length - 1]);
        }
    }
}