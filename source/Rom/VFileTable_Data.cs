using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    class VFileTable_Data
    {
        //oot
        class Container
        {
            public string TableId { get; set; }
            public int TableLength { get; set; }
            public int Records { get; set; }
            //int Offset { get; }
        }

        Container GameOvls;
        Container PlayerPause;
        Container Actors;
        Container Particles;
        Container Objects;
        Container Scenes;

        public VFileTable_Data(RomVersion version)
        {
            if (version == Game.OcarinaOfTime)
            {
                GameOvls = new Container    { TableId = "GameContextTable_Start",       TableLength = 0x30, Records = 6 };
                PlayerPause = new Container { TableId = "PlayerPauseOverlayTable_Start",TableLength = 0x1C, Records = 2 };
                Actors = new Container      { TableId = "ActorTable_Start",             TableLength = 0x20, Records = 0x1D7 };
                Particles = new Container   { TableId = "GameContextTable_Start",       TableLength = 0x1C, Records = 0x26 };
                Objects = new Container     { TableId = "ObjectTable_Start",            TableLength = 0x08, Records = 0x192 };
                Scenes = new Container      { TableId = "SceneTable_Start",             TableLength = 0x14, Records = (version == ORom.Build.DBGMQ) ? 109 : 101 };
            }
            else if (version == Game.MajorasMask)
            {
                GameOvls = new Container { TableId = "GameContextTable_Start", TableLength = 0x30, Records = 0 };
                PlayerPause = new Container { TableId = "PlayerPauseOverlayTable_Start", TableLength = 0x1C, Records = 2 };
                Actors = new Container { TableId = "ActorTable_Start", TableLength = 0x20, Records = 0x1D7 };
                Particles = new Container { TableId = "GameContextTable_Start", TableLength = 0x1C, Records = 0x26 };
                Objects = new Container { TableId = "ObjectTable_Start", TableLength = 0x08, Records = 0x192 };
                Scenes = new Container { TableId = "SceneTable_Start", TableLength = 0x10, Records =  0 };
            }
        }
    }
}
