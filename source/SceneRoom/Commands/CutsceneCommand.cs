﻿using mzxrules.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class CutsceneCommand : SceneCommand, IDataCommand
    {
        struct Record
        {
            public SegmentAddress Cutscene;
            public short EntranceIndex;
            public byte Spawn;
            public byte EventFlag;

            public Record(BinaryReader br)
            {
                Cutscene = br.ReadBigInt32();
                EntranceIndex = br.ReadBigInt16();
                Spawn = br.ReadByte();
                EventFlag = br.ReadByte();
            }

            public override string ToString()
            {
                return $"Cutscene Offset {Cutscene}, {EntranceIndex:X8} {Spawn} {EventFlag:X2}";
            }
        }
        public SegmentAddress SegmentAddress { get; set; }
        public int CutsceneAddress
        {
            get { return SegmentAddress.Offset; }
            set { SegmentAddress = new SegmentAddress(SegmentAddress, value); }
        }

        List<Record> records = new List<Record>();
        
        private Game game;

        public CutsceneCommand(Game game) => this.game = game;

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
        }

        public override string Read()
        {
            return ToString();
        }
        public override string ToString()
        {
            if (game == Game.MajorasMask)
            {
                string result = $"Cutscene list starts at {CutsceneAddress:X8}";
                foreach (var item in records)
                {
                    result += $"{Environment.NewLine}" + item;
                }
                return result;
            }

            return $"Cutscene starts at {CutsceneAddress:X8}";
        }

        public void Initialize(BinaryReader br)
        {
            if (game ==  Game.MajorasMask)
            {
                br.BaseStream.Position = SegmentAddress.Offset;
                records.Add(new Record(br));
            }
        }
    }
}
