using mzxrules.Helper;
using System;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class EnvironmentSettingsCommand : SceneCommand, ISegmentAddressAsset
    {
        public SegmentAddress SegmentAddress { get; set; }
        public int EnvironmentSettingsAddress { get { return SegmentAddress.Offset; } set { SegmentAddress.Offset = value; } }
        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            SegmentAddress = Command.Data2;
            if (SegmentAddress.Segment != (byte)ORom.Bank.scene)
                throw new Exception();
        }
        public void Initialize(System.IO.BinaryReader br)
        {
        }
        public override string ReadSimple()
        {
            return string.Format("There are {0} environment setting(s). List starts at {1:X8}.",
                Command[1], EnvironmentSettingsAddress);
        }
        public override string Read()
        {
            return ReadSimple();
        }
    }
}
