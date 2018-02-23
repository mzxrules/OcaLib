﻿using mzxrules.Helper;
using mzxrules.OcaLib.Actor;
using System;

namespace mzxrules.ZActor.OActors
{
    class BasicSwitchActor : ActorRecord_Wrapper, ISwitchFlag
    {
        byte type;
        bool frozen;
        bool blueSwitch;
        bool toggle;
        bool unknown;
        public SwitchFlag Flag { get { return flag; } set { flag = value; } }
        SwitchFlag flag;

        public BasicSwitchActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            type        = Shift.AsByte(Variable, 0x000F);
            frozen      = Shift.AsBool(Variable, 0x0080);
            unknown     = Shift.AsBool(Variable, 0x0040);
            blueSwitch  = Shift.AsBool(Variable, 0x0020);
            toggle      = Shift.AsBool(Variable, 0x0010);
            flag        = Shift.AsByte(Variable, 0x3F00);
        }
        protected override string GetActorName()
        {
            string typeName;
            switch (type)
            {
                case 0: typeName = "Floor"; break;
                case 1: typeName = "Rusted"; break;
                case 2: typeName = "Eye"; break;
                case 3: typeName = "Crystal"; break;
                case 4: typeName = "Target Crystal"; break;
                default: typeName = "Unknown"; break;
            }
            return typeName + " Switch";
        }

        protected override string GetVariable()
        {
            return String.Format("Ice? {0}, ?? {1}, Blue? {2}, Toggle? {3}, {4}",
                  frozen ? "yes" : "no",
                  unknown ? "yes" : "no",
                  blueSwitch ? "yes" : "no",
                  toggle ? "yes" : "no",
                  flag.ToString());
        }


        public SwitchFlagAttributes GetFlagAttributes()
        {
            return SwitchFlagAttributes.WriteSwitch;
        }
    }
}