using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class SkullwalltulaActor : ActorRecord
    {
        byte type;
        byte group;
        byte flag;
        public SkullwalltulaActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type = Pack.AsByte(Variable, 0xE000);
            group = Pack.AsByte(Variable, 0x1F00);
            flag = Pack.AsByte(Variable, 0x00FF);
        }
        protected override string GetActorName()
        {
            if (type == 0)
                return "Skullwalltula";
            else
                return "Gold Skulltula";
        }
        protected override string GetVariable()
        {
            return string.Format("Type {0}, Group {1:X2}, Flag {2:X2}", type, group, flag);
        }
    }
}
