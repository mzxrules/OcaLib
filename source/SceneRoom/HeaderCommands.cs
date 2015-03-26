namespace mzxrules.OcaLib.SceneRoom.Commands
{
    public enum HeaderCommands
    {
        PositionList = 0x00,
        ActorList = 0x01,
        Collision = 0x03,
        MapList = 0x04,
        CMD05 = 0x05,
        EntranceDefs = 0x06,
        SpecialObject = 0x07,
        MapBehavior = 0x08,
        MapMesh = 0x0A,
        ObjectList = 0x0B,
        PathList = 0x0D,
        TransitionActorList = 0x0E,
        EnvironmentSettings = 0x0F,
        TimeSettings = 0x10,
        SkyboxSettings = 0x11,
        SkyboxModifier = 0x12,
        ExitList = 0x13,
        End = 0x14,
        SoundSettings = 0x15,
        SoundSettingsEcho = 0x16,
        Cutscene = 0x17,
        AlternateHeaders = 0x18,
        JpegBackground = 0x19,
    }
}