namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class SkyboxModifierCommand : SceneCommand
    {
        public override string Read()
        {
            return $"Skybox Settings: Disable Sky? {Command.Data2 >> 24 > 0}, Disable Sun/Moon? {(Command.Data2 & 0xFF0000) >> 16 > 0}";
        }
        public override string ReadSimple()
        {
            return string.Format("Data1 {1:X2}, Skybox Modifier {0:X8}",
                Command.Data2,
                Command.Data1);
        }
    }
}
