namespace mzxrules.OcaLib.SceneRoom
{
    public class SceneCommand : AbstractSceneCommand
    {
        public override string Read()
        {
            return ReadRaw();
        }

        public override string ReadSimple()
        {
            return Read();
        }

        public string ReadRaw()
        {
            return $"{ID:X2} {Command.Data1:X2} {Command.Data2:X8}";
        }

        public override void SetCommand(SceneWord command)
        {
            Command = command;
        }
    }
}
