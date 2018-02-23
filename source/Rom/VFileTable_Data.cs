﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    public class VFileTable_Data
    {
        //oot
        public class Table
        {
            public string Id { get; set; }
            public int Length { get; set; }
            public int Records { get; set; }
            public int StartOff { get; set; }
            //int Offset { get; }
        }

        public Table GameOvls = new Table();
        public Table PlayerPause = new Table(); 
        public Table Actors = new Table();
        public Table Particles = new Table();
        public Table Objects = new Table();
        public Table Scenes = new Table();
        public Table TitleCards = new Table();
        public Table HyruleSkybox = new Table();

        public VFileTable_Data()
        {

        }

        public VFileTable_Data(RomVersion version)
        {
            if (version == Game.OcarinaOfTime)
            {
                int scenes = (version == ORom.Build.DBGMQ) ? 109 : 101;
                GameOvls = new Table    { Id = "GameContextTable_Start",        Length = 0x30, StartOff = 4, Records = 6 };
                PlayerPause = new Table { Id = "PlayerPauseOverlayTable_Start", Length = 0x1C, StartOff = 4, Records = 2 };
                Actors = new Table      { Id = "ActorTable_Start",              Length = 0x20, StartOff = 0, Records = 0x1D7 };
                Particles = new Table   { Id = "ParticleTable_Start",           Length = 0x1C, StartOff = 0, Records = 0x25 };
                Objects = new Table     { Id = "ObjectTable_Start",             Length = 0x08, StartOff = 0, Records = 0x192 };
                Scenes = new Table      { Id = "SceneTable_Start",              Length = 0x14, StartOff = 0, Records = scenes };
                TitleCards = new Table  { Id = "SceneTable_Start",              Length = 0x14, StartOff = 8, Records = scenes };
                HyruleSkybox = new Table{ Id = "HyruleSkyboxTable_Start",       Length = 0x08, StartOff = 0, Records = 0 };
            }
            else if (version == Game.MajorasMask)
            {
                GameOvls = new Table    { Id = "GameContextTable_Start",        Length = 0x30, StartOff = 0, Records = 0 };
                PlayerPause = new Table { Id = "PlayerPauseOverlayTable_Start", Length = 0x1C, StartOff = 0, Records = 2 };
                Actors = new Table      { Id = "ActorTable_Start",              Length = 0x20, StartOff = 0, Records = 0x2B2 };
                Particles = new Table   { Id = "ParticleTable_Start",           Length = 0x1C, StartOff = 0, Records = 0x27 };
                Objects = new Table     { Id = "ObjectTable_Start",             Length = 0x08, StartOff = 0, Records = 0x192 };
                Scenes = new Table      { Id = "SceneTable_Start",              Length = 0x10, StartOff = 0, Records =  0x71 };
            }
        }
    }
}
