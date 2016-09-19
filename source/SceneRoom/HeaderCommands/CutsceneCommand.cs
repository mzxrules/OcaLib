using mzxrules.Helper;
using System;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class CutsceneCommand : SceneCommand//, IBankRefAsset
    {
        public long Offset { get; set; }
        public long CutsceneAddress { get { return Offset; } set { Offset = value; } }
        public bool ContainsCutscene = false;
        Cutscenes.Cutscene cutscene;

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            if (command[4] == (byte)ORom.Bank.scene)
            {
                CutsceneAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
        }

        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Cutscene starts at {0:X8}", CutsceneAddress);
        }

        public void Initialize(BinaryReader br)
        {
            long seekback = br.BaseStream.Position;
            br.BaseStream.Position = CutsceneAddress;
            cutscene = new Cutscenes.Cutscene(br.BaseStream);
            br.BaseStream.Position = seekback;
        }
    }
}
