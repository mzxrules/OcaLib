using mzxrules.Helper;
using System;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class EnvironmentSettingsCommand : SceneCommand, IBankRefAsset
    {
        public long Offset { get; set; }
        public long EnvironmentSettingsAddress { get { return Offset; } set { Offset = value; } }
        public override void SetCommand(SceneWord command)
        {
            base.SetCommand(command);
            if (command[4] == (byte)ORom.Bank.scene)
            {
                EnvironmentSettingsAddress = (Endian.ConvertInt32(command, 4) & 0xFFFFFF);
            }
            else
            {
                throw new Exception();
            }
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
