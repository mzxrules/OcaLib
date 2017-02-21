namespace mzxrules.OcaLib.SceneRoom
{
    public class SceneCommand : AbstractSceneCommand
    {
        public override string Read()
        {
            return string.Format("{0:X2} {1:X2} {2:X8}", ID, Command.Data1, Command.Data2);
        }

        public override string ReadSimple()
        {
            return Read();
        }

        public override void SetCommand(SceneWord command)
        {
            Command = command;
        }
    }
}
