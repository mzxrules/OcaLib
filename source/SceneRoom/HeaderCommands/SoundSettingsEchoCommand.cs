namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class SoundSettingsEchoCommand : SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Sound Settings: Echo {0}", Command[7]);
        }
    }
}
