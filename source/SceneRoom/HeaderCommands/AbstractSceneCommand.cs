namespace mzxrules.OcaLib.SceneRoom
{
    public abstract class AbstractSceneCommand
    {
        public SceneWord Command { get; protected set; }
        public long OffsetFromFile { get; set; }
        public int ID { get { return Command.Code; } }

        public abstract string Read();
        public abstract string ReadSimple();
        public abstract void SetCommand(SceneWord command);
    }
}
