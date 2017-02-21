using mzxrules.Helper;
using System;
using System.IO;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class CutsceneCommand : SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int CutsceneAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public bool ContainsCutscene = false;
        Cutscenes.Cutscene cutscene;

        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
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
            return;

            long seekback = br.BaseStream.Position;
            br.BaseStream.Position = CutsceneAddress;
            cutscene = new Cutscenes.Cutscene(br.BaseStream);
            br.BaseStream.Position = seekback;
        }
    }
}
