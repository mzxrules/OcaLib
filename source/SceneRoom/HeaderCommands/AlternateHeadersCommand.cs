using mzxrules.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    public class AlternateHeadersCommand : SceneCommand, IBankRefAsset
    {
        Game Game { get; set; }
        public long HeaderListAddress { get { return Offset; } set { Offset = value; } }
        public long HeaderListEndAddress { get; set; }
        public List<SceneHeader> HeaderList = new List<SceneHeader>();
        public List<long> HeaderOffsetsList = new List<long>();
        public long Offset { get; set; }

        public AlternateHeadersCommand(Game game)
        {
            Game = game;
        }

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            if (command[4] == (byte)ORom.Bank.scene || command[4] == (byte)ORom.Bank.map)
            {
                HeaderListAddress = base.Command.Data2 & 0xFFFFFF;
            }
            else
                throw new Exception();
        }

        public void Initialize(BinaryReader br)
        {
            int headerCount;
            List<uint> headerOffsets = new List<uint>();

            //Get the number of headers
            headerCount = (int)(HeaderListEndAddress - HeaderListAddress) / 4;

            //If there are an impossibly high number of headers, only parse the maximum of 20
            if (headerCount >= 0x14)
                headerCount = 0x14;//throw new ArgumentOutOfRangeException();

            //Read the header offset list
            br.BaseStream.Position = HeaderListAddress;
            for (int i = 0; i < headerCount; i++)
            {
                headerOffsets.Add(br.ReadBigUInt32());
            }


            //For every header
            foreach (uint headerOffset in headerOffsets)//int i = 0; i < headerCount; i++)
            {
                //Capture the Bank Number and offset to the headers
                var bank = (headerOffset & 0xFF000000) >> 24;
                var relOffset = headerOffset & 0xFFFFFF;

                if (!(bank == 02 || bank == 03))
                    if (!(headerOffset == 0))
                        break;

                if (relOffset != 0)
                {
                    HeaderList.Add(new SceneHeader(Game));
                    HeaderOffsetsList.Add(relOffset);
                }
                else
                {
                    HeaderList.Add(null);
                    HeaderOffsetsList.Add(0);
                }
            }
        }

        public void SpiritHack(long cs0, long cs1)
        {
            for (int i = 0; i < 3; i++)
            {
                HeaderList.Add(null);
                HeaderOffsetsList.Add(0);
            }
            HeaderList.Add(new SceneHeader(Game.OcarinaOfTime));
            HeaderOffsetsList.Add(cs0);
            HeaderList.Add(new SceneHeader(Game.OcarinaOfTime));
            HeaderOffsetsList.Add(cs1);
        }

        public override string Read()
        {
            return ReadSimple();
        }

        public override string ReadSimple()
        {
            return string.Format("Alternate headers start at {0:X8}.", HeaderListAddress);
        }
    }
}
