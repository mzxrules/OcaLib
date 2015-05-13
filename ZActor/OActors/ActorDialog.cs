﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.ZActor.OActors
{
    static class ActorDialog
    {
        //Chest Rewards, Indirect
        #region static string[] Dialog000A
        static string[] Actor000A = 
        {
            "Glitched",
            "Bombs",
            "Deku Nuts",
            "Bombchus (10)",
            "Fairy bow",
            "Fairy Slingshot",
            "Boomerang",
            "Deku stick",
            "Hookshot",
            "Longshot",
            "Lens of Truth",
            "Zelda’s letter",
            "Ocarina of Time",
            "Megaton hammer",
            "Cojiro",
            "Empty Bottle",
            "Red Potion",
            "Green Potion",
            "Blue Potion",
            "Bottled Fairy",
            "Lon Lon Milk",
            "Ruto's Letter",
            "Magic Beans",
            "Skull Mask",
            "Spooky Mask",
            "Cucco w/ magic bean text.",
            "Keaton Mask",
            "Bunny Hood",
            "Mask of Truth",
            "Pocket Egg",
            "Pocket Cucco w/ magic bean text.",
            "Odd Mushroom",
            "Odd Potion",
            "Poacher’s Saw",
            "Broken Goron’s Sword",
            "Prescription",
            "Eyeball Frog",
            "World’s Finest Eye Drops",
            "Claim Check",
            "Kokiri’s Sword",
            "Giant’s Knife",
            "Deku Shield",
            "Hylian Shield",
            "Mirror Shield",
            "Goron Tunic",
            "Zora Tunic",
            "Iron Boots",
            "Hover Boots",
            "Big Quiver",
            "Biggest Quiver",
            "Bomb Bag",
            "Big Bomb Bag",
            "Biggest Bomb Bag",
            "Silver Gauntlets",
            "Golden Gauntlets",
            "Silver Scale",
            "Golden Scale",
            "Stone of Agony",
            "Gerudo Membership Card",
            "Fairy Ocarina?",
            "Blue Rupee",
            "Heart Container",
            "Piece of Heart",
            "Boss Key",
            "Compass",
            "Map",
            "Small Key",
            "Blue Rupee",
            "Blue Rupee",
            "Adult Wallet",
            "Giant Wallet",
            "Weird Egg",
            "Heart",
            "Arrows (5)",
            "Arrows (10)",
            "Arrows (15)",
            "Green Rupee",
            "Blue Rupee",
            "Red Rupee",
            "Heart Container",
            "Lon Lon Milk",
            "Goron Mask",
            "Zora Mask",
            "Gerudo Mask",
            "Goron Bracelet",
            "Purple Rupee",
            "Yellow Rupee",
            "Biggoron’s Sword",
            "Fire Arrow",
            "Ice Arrow",
            "Light Arrow",
            "Gold Skulltula token",
            "Din’s Fire",
            "Farore’s Wind",
            "Nayru’s Love",
            "Deku Seeds Bullet Bag (40)",
            "Deku Seeds Bullet Bag (40)",
            "Deku stick",
            "Deku stick",
            "Deku nuts",
            "Deku nuts",
            "Bombs",
            "Bombs",
            "Bombs",
            "Bombs",
            "Blue Rupee",
            "Bombchus (5)",
            "Bombchus (20)",
            "Bottled Fish",
            "Bottled Bugs",
            "Blue Fire",
            "Bottled Poe",
            "Bottled Giant Poe",
            "Door Key (Chest Game)",
            "Loser Green Rupee (Chest Game)",
            "Loser Blue Rupee (Chest Game)",
            "Loser Red Rupee (Chest Game)",
            "Winner Purple Rupee (Chest Game)",
            "Winner Piece of Heart",
            "Deku Stick Upgrade (20)",
            "Deku Stick Upgrade (30)",
            "Deku Nut Upgrade (30)",
            "Deku Nut Upgrade (40)",
            "Deku Seeds Bullet Bag (50)",
            "Ice Trap",
            "Pocket Egg (no model)",
            "Crashes",
            "Crashes"
        };
        #endregion

        //start offset 0100 in text bank
        #region static string[] Dialog011B
        static string[] Dialog0100 = new string[]
        {
            "What’s that?",
            "Look, look, LINK! You can see down below this web using C-Up!",
            "Look at this wall! The vines growing on it give it a rough surface... Maybe you can climb it, LINK!",
            "You can open a door by standing in front of it and pressing A. Pay attention to what the Action Icon says. That’s the blue icon at the top of the screen!",
            "Look! Something is hanging up there! It looks like an old ladder!",
            "Hey... isn’t that the same design that’s on the Door of Time?",
            "It looks like that torch was burning not too long ago...",
            "From here on, we’ll be going through some narrow passages! If you take it slow, maybe you can sneak up on some enemies.",
            "Stand next to this block and grab hold of it with A. While holding A, you can push or pull it. If you stand next to the block and press A while pressing [the control stick] towards the block, you can climb on top of it. Pay attention to what the Action Icon says!",
            "0109",
            "010a",
            "010b",
            "After you get into the water, if you hold down A, you can dive! I bet there are some interesting things underwater!",
            "010d",
            "010e",
            "010f",
            "0110",
            "0111",
            "0112",
            "0113",
            "Wow! Look at all those Bomb Flowers! Is there any way you can set them all off at once?",
            "It looks like there are many lava pits around here, so watch your step!",
            "With that switch on, the moving platform goes even higher. Now you can quickly reach the second floor!",
            "0117",
            "0118",
            "You never know what will be around the corner in these narrow paths... Use Z Targeting to always look in the proper direction. This is a useful technique, isn’t it?",
            "011a",
            "011b",
            "011c",
            "011d",
            "011e",
            "LINK, what are you looking at?",
            "0120",
            "0121",
            "0122",
            "0123",
            "The Desert Colossus’s face... it sure looks evil!",
            "0125",
            "I can hear the spirits whispering in this room... “Look for the eye of truth...” That’s what they’re saying!",
            "0127",
            "Here... I can hear the spirits whispering in this room... “Those who have sacred feet should let the wind guide them. Then, they will be lead to the hidden path.” That’s what they are saying!",
            "This wall... it says something here... “Danger above...” That’s what it says.",
            "This wall... it says something here... “Danger below...” That’s what it says.",
            "The water flowing out of this statue is flooding the entire floor.",
            "012c",
            "012d",
            "012e",
            "Watch out, LINK! Electricity is running through this green slimy thing!",
            "0130",
            "Watch out, LINK! Electricity is running through this red slimy thing!",
            "Watch out, LINK! Electricity is running through this blue slimy thing!",
            "This switch... It doesn’t look like you can press it down with your weight alone, LINK...",
            "0134",
            "0135",
            "0136",
            "The red slimy thing is gone! That must be because you cut the red tail! Will that work with the other ones too?",
            "0138",
            "There’s a switch beyond this wall!",
            "It looks like there is something up there on top of the platform!",
            "013b",
            "013c",
            "WHAAAT!? Look at all those flags! Can you figure out which ones are real? (Beta?)",
            "013e",
            "013f",
            "The Great Deku Tree has summoned you! Please come with me!",
            "C’mon! Be brave! Let’s go into the Deku Tree!",
            "The Great Deku Tree wanted us to go visit the princess at Hyrule Castle... Shouldn’t we get going?",
            "The girl from the ranch asked us to find her father... I wonder where he is?",
            "I wonder where we’ll find the princess in this big old castle?",
            "What would Saria say if we told her we’re going to save Hyrule?",
            "Impa said that the Spiritual Stone of Fire is somewhere on Death Mountain.",
            "Let’s go inside the Dodongo’s Cavern using a Bomb Flower!",
            "Darunia said that a fairy lives on top of Death Mountain, didn’t he?",
            "I wonder if Saria knows anything about the other Spiritual Stone?",
            "It seems Princess Ruto somehow got inside Jabu-Jabu’s belly...",
            "You collected three Spiritual Stones! Let’s go back to Hyrule Castle!",
            "Those people on the white horse... they were Zelda and Impa, weren’t they? It looked like they threw something into the moat!",
            "Let’s go check inside the Temple of Time.",
            "Should we believe what Sheik said and go to Kakariko Village?",
            "014f",
            "I wonder what’s going on in the forest right now... I’m worried about Saria, too!",
            "That cloud over Death Mountain... there is something strange about it...",
            "An arctic wind is blowing from Zora’s River... do you feel it?",
            "Those Iron Boots look like they weigh a ton! If you wear those boots, you may be able to walk at the bottom of the lake.",
            "Let’s look for someone who might know about the other Sages!",
            "That monster! It came out of the well in the village! Let’s go check out the well!",
            "I wonder who built the Spirit Temple, and for what purpose?",
            "Have you ever played the Nocturne of Shadow that Sheik taught you?",
            "The desert... that is where Ganondorf the Evil King was born. If we go there, we might find something...",
            "0159",
            "Equip the Silver Gauntlets and try to move things you couldn’t budge before!",
            "The one who is waiting for us at the Temple of Time...it could be...",
            "We have to save Princess Zelda from her imprisonment in Ganon’s Castle!",
            "015D",
            "015E",
            "LINK, try to keep moving!!",
            "I don’t mind talking to you using the Ocarina’s magic, but I’d really like to talk to you face-to-face!",
            "The forest is connected to many different places! If you can hear my song, you must be near somewhere that is connected to the forest!",
            "I was so happy to hear that Mr. Darunia loved my song so much! I was even happier to find out I helped out on your quest, LINK! Tee hee hee!",
            "Are you collecting Spiritual Stones? You have one more to find? You mean the Spiritual Stone of Water, don’t you? The Great Deku Tree once told me that King Zora, ruler of Zora’s Domain, has it...",
            "Are you collecting Spiritual Stones? You have one more to find? You mean the Spiritual Stone of Fire, don’t you? The Great Deku Tree once told me that Mr. Darunia of the Gorons has it...",
            "LINK... I don’t know what it is... I have this feeling of dread... The Castle... Yes, something bad is happening at the Castle!",
            "What? Your ocarina sounds... different somehow... Have you been practicing a lot, LINK?",
            "Are you looking for a temple? A mysterious bird once told me... “Eyes that can see through darkness will open in a storm.” Do you have any idea what he meant by this?",
            "Where are you, LINK? Are you looking for a temple? I once heard a mysterious bird say... “Go, young man. Go to the Desert Goddess with an ocarina.” Do you have any idea what he meant by this?",
            "Did you find all the temples yet?",
            "Great! You’re safe! I knew I would hear from you again! I’m in the Forest Temple! The forest spirits were calling for help, so I went to check it out... But it’s full of evil monsters! Help me, LINK!",
            "LINK... At first, I didn’t want to become the Sage of the Forest... But I’m glad now. Because I’m helping you save Hyrule, LINK! Yes, I am!",
            "If all six Sages come together, we can imprison Ganondorf, the King of Evil, in the Sacred Realm. But in order to make a perfect seal, we need the seventh Sage. Someone you know must be that Sage, LINK... From now on, you must travel between past and future to awaken the remaining Sages! Keep up the good work, LINK!",
            "We, the Six Sages, are channeling our power to you! The destiny of Hyrule depends upon you!",
            "016e",
            "016f",
            "You borrowed a Pocket Egg! A Pocket Cucco will hatch from it overnight. Be sure to give it back when you are done with it."
        };

        static string[] Dialog0180 = new string[]
        {
            "I can hear a voice from / somewhere... / It's saying: / \"Collect five silver Rupees...\"",
            "This wall...it's saying something! / It says: / If you want to see a ferry to the / other world, come here...",
            "0182",
            "If you want to ride that boat, be  / careful! It looks very old... Who / knows when it might sink?",
            "There is a door over here... Is  / there any way to get across?",
            "0185",
            "That red ice...it's so weird!",
            "0187",
            "0188",
            "This blue fire...it doesn't seem / natural. Maybe you can use it for / something?",
            "018a",
            "018b",
            "The fires on the torches are gone. / Seems like the ghosts took them  / away!",
            "Look, Link! A torch / is lit! That's because / you beat a ghost, isn't it?!",
            "018e",
            "There are arrows painted on the  / floor!",
            "This corridor is all twisted!",
            "Watch for the shadows of / monsters that hang from the / ceiling.",
            "There's a treasure chest here.",
            "0193",
            "This...this is the same torch we / saw at the entrance to the temple, / isn't it?",
            "This torch is lit...that means...",
            "0196",
            "This switch is frozen!",
            "Link, watch out! / The ceiling is falling down!",
            "0199",
            "019a",
            "019b",
            "019c",
            "019d",
            "019e",
            "019f",
            "01a0",
            "01a1",
            "01a2",
            "Link, I hear Goron  / voices down below.",
            "01a4",
            "You can see down from here... / Isn't that the room where we saw / Darunia?",
            "01a6",
            "This statue...haven't we seen it / somewhere before?",
            "01a8",
            "This switch looks rusted.",
            "01aa",
            "Link! Be careful! / Don't get swallowed by the  / vortexes!",
            "01ac",
            "01ad",
            "01ae",
            "01af",
            "01b0",
            "01B1"
        };

        #endregion

        //???
        #region static string[] Dialog0185
        static string[] Dialog0200 = new string[]
        {
            "Hi! I’m a talking door! (BETA)",
            "Strange... this door doesn’t open...",
            "Strong iron bars are blocking the door. You can’t open them with your hands!",
            "You need a Key to open a door that is locked or chained.",
            "You need a special key to open this door.",
            "Be quiet! It’s only [time]! I, Dampé the Gravekeeper, am in bed now! Go away and play! Maybe you can find a ghost in the daytime?",
            "It’s [time] now. The Gravedigging Tour is over now! I, Dampé the gravekeeper, am in bed! Go away and play! Maybe you’ll find a ghost!",
            "Happy Mask Shop / Please read this sign before you use this shop...",
            "Shadow Temple... here is gathered Hyrule’s bloody history of greed and hatred...",
            "What is hidden in the darkness... Tricks full of ill will... You can’t see the way forward...",
            "One who gains the eye of truth will be able to see what is hidden in the darkness.",
            "Something strange is covering the entrance. You must solve the puzzle in this room to make the entrance open.",
            "Giant dead Dodongo... when it sees red, a new way to go will be open.",
            "Treasure Chest Contest / Temporarily Closed / Open Tonight!",
            "Medicine Shop / Closed until morning...",
            "Shooting Gallery / Open only during the day",
            "Happy Mask Shop / Now hiring part-time / Apply during the day",
            "Bazaar / Open only during the day",
            "Show me the light!",
            "One with the eye of truth shall be guided to the Spirit Temple by an inviting ghost.",
            "Those who wish to open the path sleeping at the bottom of the lake must play the song passed down by the Royal Family.",
            "Those who wish to open the gate on the far heights, play the song passed down by the Royal Family.",
            "Those who find a Small Key can advance to the next room. Those who don’t can go home! (BETA)",
            "If you wish to speak to me, do so from the platform.",
            "Hi, LINK! Look this way! Look over here with Z, and talk to me with A.",
            "The current time is: [time].",
            "Shine light on the living dead...",
            "Those who break into the Royal Tomb will be obstructed by the lurkers in the dark.",
            "Hey, you! Young man, over there! Look over here, inside the cell!",
            "My little boy isn’t here right now... I think he went to play in the graveyard...",
            "Oh, my boy is asleep right now. Please come back some other time to play with him!",
            "When water fills the lake, shoot for the morning light.",
            "If you want to travel to the future, you should return here with the power of silver from the past.",
            "If you want to proceed to the past, you should return here with the pure heart of a child.",
            "This door is currently being refurbished. (BETA)",
            "It looks like something used to be set in this stand...",
            "Make my beak face the skull of truth. The alternative is descent into the deep darkness.",
            "This is not the correct key... the door won’t open!",
            "Granny’s Potion Shop / Closed / Gone for Field Study / Please come again! –Granny",
            "Who’s there? What a bad kid, trying to enter from the rear door! Such a bad kid... I have to tell you some juicy gossip! The boss carpenter has a son... He’s the guy who sits under the tree every night... Don’t tell the boss I told you that!",
            "Look at this!",
            "Malon’s gone to sleep! I’m goin’ to sleep now, too. Come back again when it’s light out!",
            "LINK’s Records! / Spiders squished: 0 / Largest fish caught: 0 pounds / Marathon time: 00\"00\" / Horse race time: 00\"00\" / Horseback archery: 0 points",
            "The crest of the Royal Family is inscribed here.",
            "R.I.P. / Here lie the souls of those who swore fealty to the Royal Family of Hyrule / The Sheikah, guardians of the Royal Family and founders of Kakariko, watch over these spirits in their eternal slumber.",
            "Sleepless Waterfall / The flow of this waterfall serves the King of Hyrule. When the king slumbers, so too do these falls.",
            "Some frogs are looking at you from underwater...",
            "You’re standing on a soft carpet for guests... it feels so plush under your feet!",
            "If you can overcome the trials in the chambers ahead, then and only then will you be qualified to hold our secret treasure!",
            "If you desire to acquire our hidden treasure, you must strive to obtain the keys hidden in each chamber!",
            "Defeat all the enemies in a limited time!",
            "Collect the underwater gems!",
            "Cross the sea of fire!",
            "Find a secret passage in this room!",
            "Blind the eyes of the statue!",
            "One with silver hands shall move a giant block!",
            "Without the necessary items, one will be confounded by impossible mysteries.",
            "Gather the jewels of white, while avoiding traps and danger!",
            "Fishing Pond / The fish are really biting today!",
            "...???",
            "The Shadow will yield only to one with the eye of truth, handed down in Kakariko Village.",
            "You borrowed a Pocket Egg! ..."
        };
        #endregion


        public static string Get(ushort actor, ushort value)
        {
            string result;
            switch (actor)
            {
                case 0x011B: result = (value < 0x80) ?
                    /*Less than 0x80*/(value < 0x70) ? Dialog0100[value] : Dialog0100[0x70] : 
                    /*More than 0x7F*/(value < 0xB2) ? Dialog0180[value - 0x80] : Dialog0100[0x70]; break;
                case 0x0185: result = (value < 0x3D) ? Dialog0200[value] : Dialog0200[0x3D]; break;
                default: result = "Undefined"; break;
            }
            return result;
        }
        public static string GetReward(byte value)
        {
            if (value < 0x7F)
                return Actor000A[value];
            else
                return Actor000A[0x7F];
        }
    }
}
