using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZActor.OActors;
using mzxrules.OcaLib.SceneRoom.Commands;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom
{        
    #region Command Data
    
    //scene setup
    //0x00 Actors (Link)
    //0x03
    //0x04
    //0x06
    //0x07 special objects
    //0x0D Path List
    //0x0E Transition Actor List
    //0x0F
    //0x11
    //0x13
    //0x15
    //0x17
    //0x19

    //Map Setup
    //0x08 map behavior
    //0x0A mesh
    //0x0B
    //0x10 time setting
    //0x12
    //0x16 sound settings echo

    //shared
    //0x01 Actors
    //0x05 wind
    //0x18

    #endregion

    public class SceneHeader
    {
        public Game Game { get; private set; }

        public AlternateHeadersCommand Alternate
        {
            get { return (AlternateHeadersCommand)this[HeaderCommands.AlternateHeaders]; }
        }

        public SceneCommand this[HeaderCommands c]
        {
            get
            {
                return cmds.SingleOrDefault(x => x.ID == (int)c);
            }
        }

        List<SceneCommand> cmds = new List<SceneCommand>();

        public SceneHeader(Game game)
        {
            Game = game;
        }

        /// <summary>
        /// Creates an in-memory model of the scene header
        /// </summary>
        /// <param name="br">Binary reader containing the scene or room file</param>
        /// <param name="seek">offset to the start of the scene header</param>
        public void Load(BinaryReader br, long seek)
        {
            SceneWord command;
            bool KeepReading = true;
            long seekBackTop;

            seekBackTop = br.BaseStream.Position;
            br.BaseStream.Position = seek;

            while (KeepReading)
            {
                command = new SceneWord();
                br.Read(command, 0, 8);
                SetCommand((HeaderCommands)command.Code, command);
                if ((HeaderCommands)command.Code == HeaderCommands.End)
                {
                    KeepReading = false;
                }
            }
            br.BaseStream.Position = seekBackTop;

            if (HasAlternateHeaders())
            {
                Alternate.HeaderListEndAddress = AltHeaderEnd();
                Alternate.Initialize(br);
            }
        }

        public void WriteHeader(BinaryWriter bw)
        {
            foreach (SceneCommand cmd in cmds)
            {
                bw.Write(cmd.Command, 0, SceneWord.LENGTH);
            }
        }

        public void SetCommand(HeaderCommands id, SceneWord sceneWord)
        {
            SceneCommand command;
            switch (id)
            {
                case HeaderCommands.PositionList:       //0x00
                    command = new PositionListCommand();
                    break;
                case HeaderCommands.ActorList:          //0x01
                    command = new ActorListCommand(Game);
                    break;
                case HeaderCommands.Collision:          //0x03
                    command = new CollisionCommand();
                    break;
                case HeaderCommands.MapList:            //0x04
                    command = new MapListCommand();
                    break;
                case HeaderCommands.CMD05:              //0x05
                    command = new CMD05Command();
                    break;
                case HeaderCommands.EntranceDefs:       //0x06
                    command = new EntranceDefinitionsCommand();
                    break;
                case HeaderCommands.SpecialObject:      //0x07
                    command = new SpecialObjectCommand();
                    break;
                case HeaderCommands.MapBehavior:        //0x08
                    command = new MapBehaviorCommand();
                    break;
                case HeaderCommands.MapMesh:            //0x0A
                    command = new MapMeshCommand();
                    break;
                case HeaderCommands.ObjectList:         //0x0B
                    command = new ObjectListCommand();
                    break;
                case HeaderCommands.PathList:           //0x0D
                    command = new PathListCommand();
                    break;
                case HeaderCommands.TransitionActorList://0x0E
                    command = new TransitionActorListCommand(Game);
                    break;
                case HeaderCommands.EnvironmentSettings://0x0F
                    command = new EnvironmentSettingsCommand();
                    break;
                case HeaderCommands.TimeSettings:       //0x10
                    command = new TimeSettingsCommand();
                    break;
                case HeaderCommands.SkyboxSettings:     //0x11
                    command = new SkyboxSettingsCommand();
                    break;
                case HeaderCommands.SkyboxModifier:     //0x12
                    command = new SkyboxModifierCommand();
                    break;
                case HeaderCommands.ExitList:           //0x13
                    command = new ExitListCommand();
                    break;
                case HeaderCommands.End:                //0x14
                    command = new EndCommand();
                    break;
                case HeaderCommands.SoundSettings:      //0x15
                    command = new SoundSettingsCommand();
                    break;
                case HeaderCommands.SoundSettingsEcho:  //0x16
                    command = new SoundSettingsEchoCommand();
                    break;
                case HeaderCommands.Cutscene:           //0x17
                    command = new CutsceneCommand();
                    break;
                case HeaderCommands.AlternateHeaders:    //0x18
                    command = new AlternateHeadersCommand(Game);
                    break;
                case HeaderCommands.JpegBackground:     //0x19
                    command = new JpegBackgroundCommand();
                    break;
                default: command = new SceneCommand(); //throw new NotImplementedException();
                    break;
            }

            command.SetCommand(sceneWord);
            cmds.Add(command);
        }

        public void InitializeAssets(BinaryReader br)
        {
            foreach (IBankRefAsset asset in cmds.Where(x => x is IBankRefAsset && !(x is AlternateHeadersCommand)))
            {
                if (asset is ExitListCommand)
                {
                    ((ExitListCommand)asset).EndOffset = ExitListEnd();
                }
                asset.Initialize(br);
            }
        }

        public string Read()
        {
            StringBuilder s = new StringBuilder();
            HeaderCommands cmd;

            foreach (SceneCommand command in cmds)
            {
                cmd = (HeaderCommands)command.ID;
                if (cmd != HeaderCommands.End)
                {
                    s.Append(string.Format("{0:X2}: ",(int)cmd));

                    if (this[cmd] != null)
                        s.AppendLine(this[cmd].Read());
                }
                else
                    break;
            }
            return s.ToString();
        }

        #region AlternateHeaders
        public bool HasAlternateHeaders()
        {
            return (this[HeaderCommands.AlternateHeaders] != null);
        }

        /// <summary>
        /// Gets alternate setup count for scene files only
        /// </summary>
        /// <returns></returns>
        public long AltHeaderEnd()
        {
            SceneCommand sc;

            sc = this[HeaderCommands.PositionList];

            if (sc != null)
                return ((IBankRefAsset)sc).Offset;
            else if ((IBankRefAsset)this[HeaderCommands.ObjectList] != null)
                return ((IBankRefAsset)this[HeaderCommands.ObjectList]).Offset;
            else 
                return ((IBankRefAsset)this[HeaderCommands.MapMesh]).Offset;
        }
        #endregion

        public long ExitListEnd()
        {
            if (this[HeaderCommands.EnvironmentSettings] != null)
                return ((IBankRefAsset)this[HeaderCommands.EnvironmentSettings]).Offset;
            else return 0;
        }

        //HACK
        public void SpiritHackSetAlternateHeaders(long cs0, long cs1)
        {
            AlternateHeadersCommand cmd = new AlternateHeadersCommand(Game.OcarinaOfTime);
            cmd.SetCommand(new byte[] { 0x18, 0, 0, 0, /**/ 2, 0, 0, 0 });
            cmds.Add(cmd);

            Alternate.SpiritHack(cs0, cs1);
        }

        /// <summary>
        /// Gets all map addresses from the header and child headers
        /// </summary>
        /// <returns>Returns a list of maps if the MapListCommand is found, else returns null</returns>
        public List<FileAddress> GetRoomAddresses()
        {
            List<FileAddress> resultAddresses = new List<FileAddress>();
            MapListCommand cmd;

            cmd = (MapListCommand)this[HeaderCommands.MapList];
            if (cmd == null)
                return null;

            foreach (FileAddress addr in cmd.MapAddresses)
                resultAddresses.Add(addr);

            if (HasAlternateHeaders())
            {
                //for every scene setup
                foreach (SceneHeader altHeader in Alternate.HeaderList.Where(x => x != null))
                {
                    //for every map in that scene setup
                    cmd = (MapListCommand)altHeader[HeaderCommands.MapList];

                    for (int i = 0; i < cmd.Maps; i++)
                    {
                        if (!resultAddresses.Contains(cmd.MapAddresses[i]))
                        {
                            resultAddresses.Add(cmd.MapAddresses[i]);
                            break;
                        }
                    }
                }
            }
            return resultAddresses;
        }

        private IEnumerable<SceneHeader> GetAltHeaders()
        {
            return from alt in Alternate.HeaderList
                   where alt != null
                   select alt;
        }

        #region GetActors
        public List<List<ActorRecord>> GetActorsWithId(int id)
        {
            AlternateHeadersCommand altCmd;
            List<List<ActorRecord>> result;

            result = new List<List<ActorRecord>>();
            result.Add(GetActorsById(id));
            altCmd = (AlternateHeadersCommand)this[HeaderCommands.AlternateHeaders];
            if (altCmd != null)
            {
                for (int i = 0; i < altCmd.HeaderList.Count; i++)
                {
                    if (altCmd.HeaderList[i] != null)
                        result.Add(altCmd.HeaderList[i].GetActorsById(id));
                    else
                        result.Add(new List<ActorRecord>());
                }
            }
            return result;
        }

        private List<ActorRecord> GetActorsById(int id)
        {
            List<ActorRecord> result;
            result = new List<ActorRecord>();
            IEnumerable<SceneCommand> cmdQuery;

            //Linq query
            cmdQuery = from cmd in cmds
                       where cmd is IActorList
                       select cmd;

            foreach (IActorList actorList in cmdQuery)
            {
                if (id == -1)
                    result.AddRange(actorList.GetActors());
                else
                    result.AddRange(actorList.GetActors().Where(x => x.Actor == id));
            }

            return result;
        }
        #endregion

        #region GetObjects
        public List<List<ushort>> GetObjectsWithId(int id)
        {
            AlternateHeadersCommand altCmd;
            List<List<ushort>> result;

            result = new List<List<ushort>>();
            result.Add(GetObjectsById(id));
            altCmd = (AlternateHeadersCommand)this[HeaderCommands.AlternateHeaders];
            if (altCmd != null)
            {
                for (int i = 0; i < altCmd.HeaderList.Count; i++)
                {
                    if (altCmd.HeaderList[i] != null)
                        result.Add(altCmd.HeaderList[i].GetObjectsById(id));
                    else
                        result.Add(new List<ushort>());
                }
            }
            return result;
        }

        private List<ushort> GetObjectsById(int id)
        {
            List<ushort> result;
            IEnumerable<SceneCommand> cmdQuery;

            result = new List<ushort>();
            cmdQuery = from cmd in cmds
                       where cmd is ObjectListCommand
                       select cmd;

            foreach (ObjectListCommand objectList in cmdQuery)
            {
                if (id == -1)
                    result.AddRange(objectList.ObjectList);
                else
                    result.AddRange(objectList.ObjectList.Where(x => x == id));
            }
            return result;
        }
        #endregion

        internal List<SceneCommand> GetCommandWithId(int id)
        {
            List<SceneCommand> current;
            SceneCommand cmd;
            current = new List<SceneCommand>();

            cmd = this.cmds.SingleOrDefault(x => x.ID == id);
            if (cmd != null)
                current.Add(cmd);

            return current;

        }

        internal List<List<SceneCommand>> GetAllCommandsWithId(int id)
        {
            List<List<SceneCommand>> result = new List<List<SceneCommand>>();

            result.Add(GetCommandWithId(id));
            if (HasAlternateHeaders())
            {
                foreach (SceneHeader h in Alternate.HeaderList)
                {
                    if (h == null)
                        result.Add(new List<SceneCommand>());
                    else
                        result.Add(h.GetCommandWithId(id));
                }
            }
            return result;
        }

        public List<SceneCommand> Commands()
        {
            return cmds;
        }

        //FIXME
        //public void SetExits(FileStream sr)
        //{
        //    byte[] bShort = new byte[2];
        //    int exitCount = 0;
        //    MaxExits = 0;

        //    if (header.HasAlternateSetups())
        //    {
        //        foreach (SceneSetup setup in header.Alt.AlternateSetups)
        //        {
        //            if (setup != null && setup.ExitListAddress != -1)
        //            {
        //                if (setup.EnvironmentSettingsAddress == -1)
        //                {
        //                    throw new Exception("EnvSetting always set" + ID);
        //                }
        //                else
        //                {
        //                    exitCount = (int)((setup.EnvironmentSettingsAddress - setup.ExitListAddress) / 2);
        //                    if (MaxExits < exitCount)
        //                        MaxExits = exitCount;
        //                }
        //            }
        //        }
        //        foreach (SceneSetup setup in header.Alt.AlternateSetups)
        //        {
        //            if (setup != null)
        //            {
        //                if (setup.ExitListAddress != -1)
        //                {
        //                    sr.Position = setup.ExitListAddress;
        //                    for (int i = 0; i < MaxExits; i++)
        //                    {
        //                        sr.Read(bShort, 0, 2);
        //                        setup.exitList.Add((ushort)((bShort[0] << 8) + bShort[1]));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //FIXME
        //public string ReturnCSVEntranceDefinitions()
        //{
        //    EntranceDef def;
        //    Vector3<short> v;
        //    StringBuilder result = new StringBuilder();
        //    SceneSetup setup;

        //    for (int i = 0; i < AlternateSetups.Count; i++)
        //    {
        //        if (AlternateSetups[i] != null)
        //        {
        //            for (int j = 0; j < MaxEntrances; j++)
        //            {
        //                setup = (SceneSetup)AlternateSetups[i];
        //                def = setup.EntranceDefinitions[j];
        //                result.AppendFormat("{0}, {1}, {2}, {3}, {4}",
        //                    ID, i, j, def.Map, def.Position);
        //                if (def.Position < setup.Positions)
        //                {
        //                    v = setup.PositionList[def.Position].GetCoords();
        //                    result.AppendFormat(", {0}, {1}, {2}",
        //                        v.x, v.y, v.z);
        //                }
        //                result.AppendLine();
        //            }
        //        }
        //    }
        //    return result.ToString();
        //}

        //FIXME
        //public string ReturnCSVExitDefinitions()
        //{
        //    ushort exit;
        //    StringBuilder result = new StringBuilder();
        //    SceneSetup setup;
        //    for (int i = 0; i < header.Alt.AlternateSetups.Count; i++)
        //    {
        //        if (header.Alt.AlternateSetups[i] != null)
        //        {
        //            setup = (SceneSetup)header.Alt.AlternateSetups[i];
        //            if (setup.ExitListAddress != -1)
        //            {
        //                for (int j = 0; j < MaxExits; j++)
        //                {
        //                    exit = setup.exitList[j];
        //                    result.AppendFormat("{0}, {1}, {2}, {3}",
        //                        ID, i, j,
        //                        exit.ToString("X4"));
        //                    result.AppendLine();
        //                }
        //            }
        //        }
        //    }
        //    return result.ToString();
        //}
    }
}
