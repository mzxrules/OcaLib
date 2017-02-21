namespace mzxrules.OcaLib.SceneRoom.Commands
{
    class SpecialObjectCommand: SceneCommand
    {
        public override string Read()
        {
            return ReadSimple();
        }
        public override string ReadSimple()
        {
            return string.Format("Elf_Message {0}, Load Object {1:X4}",
                Command.Data1,
                Command.Data2);
        }
    }
}
