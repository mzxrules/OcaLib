using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    class ChestActor : ActorRecord_Wrapper
    {
        static string[] ChestTypeDefs = new string[]
        {
            "Large",
            "Large, Appears, Clear Flag",
            "Boss Key’s Chest",
            "Large, Falling, Switch Flag",
            "Large, Invisible",
            "Small",
            "Small, Invisible",
            "Small, Appears, Clear Flag",
            "Small, Falls, Switch Flag",
            "Large, Appears, Zelda's Lullaby",
            "Large, Appears, Sun's Song Triggered",
            "Large, Appears, Switch Flag",
            "Large"
        };

        byte chestType;
        byte rewardItem;
        ChestFlag chestFlag;
        SwitchFlag sceneFlag;
        public ChestActor(byte[] record, params int[] p)
            : base(record, p)
        {
            chestType = Pack.AsByte(Variable, 0xF000);
            rewardItem = Pack.AsByte(Variable, 0x0FE0); //(byte)((Variable & 0xFF0) >> 5);
            chestFlag =  Pack.AsByte(Variable, 0x001F);
            sceneFlag = Pack.AsByte(rotation.z, 0x003F);
        }
        protected override string GetActorName()
        {
            return "Chest";
        }
        protected override string GetVariable()
        {

            return String.Format("{0}, reward {1:X2}: {2}, {3}, bound to {4}",
                ChestTypeDefs[(chestType < 0xC) ? chestType : 0xC],
                  rewardItem,
                  ActorDialog.GetReward(rewardItem),
                  chestFlag,
                  sceneFlag);
        }
    }
}
