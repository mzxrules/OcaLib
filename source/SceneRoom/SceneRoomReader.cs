using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZActor.OActors;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom
{
    /// <summary>
    /// A gross class that was written to initially manage VerboseOcarina's core functionality
    /// </summary>
    public static class SceneRoomReader
    {
        public static Scene InitializeScene(int number, RomFile file)
        {
            BinaryReader br;
            Scene scene = null;

            if (file == null)
                return scene;

            scene = new Scene(file.Version.Game, number, file.Record.VirtualAddress);
            br = new BinaryReader(file);

            //if (LocalFileTable.Version == ORom.Build.N0
            //    && number == 6)
            //    SpiritHack.LoadSpiritSceneHeader(br, scene);
            //else
                LoadISceneRoomHeader(br, scene);
            return scene;
        }

        public static Scene InitializeScene(int number, BinaryReader br, Game game)
        {
            Scene scene;
            scene = new Scene(game, number, new FileAddress());
            LoadISceneRoomHeader(br, scene);
            return scene;
        }

        public static Room InitializeRoom(RomFile file)
        {
            BinaryReader br;
            Room newRoom = new Room(file.Version.Game, file.Record.VirtualAddress);

            br = new BinaryReader(file);
            LoadISceneRoomHeader(br, newRoom);
            return newRoom;
        }

        //public static Room LoadSpiritRoom(FileAddress addr, int roomNo)
        //{
        //    BinaryReader br;
        //    Room item = new Room(LocalFileTable.Version.Game, addr);

        //    br = new BinaryReader(LocalFileTable.GetFile(item.VirtualAddress.Start));
        //    SpiritHack.LoadSpiritRoomHeader(br, item, roomNo);
        //    return item;
        //}
        
        #region InitializeMembers
        
        private static void LoadISceneRoomHeader(BinaryReader br, ISceneRoomHeader item)
        {
            SceneHeader header;
            header = item.Header;

            //Load the root header
            header.Load(br, 0);
            header.InitializeAssets(br);

            if (header.HasAlternateHeaders())
            {
                for (int i = 0; i < header.Alternate.HeaderList.Count; i++)
                {
                    if (header.Alternate.HeaderList[i] != null)
                    {
                        header.Alternate.HeaderList[i].Load(br, header.Alternate.HeaderOffsetsList[i]);
                        header.Alternate.HeaderList[i].InitializeAssets(br);
                    }
                }
            }
        }

        #endregion

        public static bool TryGetCutscene(Rom rom, long address, out Cutscenes.Cutscene cutscene)
        {
            FileRecord addr;
            cutscene = null;

            addr = rom.Files.GetFileStart(address);
            if (addr == null)
                return false;
            var s = (Stream)rom.Files.GetFile(addr.VirtualAddress);
            s.Position = addr.GetRelativeAddress(address);
            cutscene = new Cutscenes.Cutscene(s);
            return true;
        }

        public static string ReadScene(Scene scene)
        {
            StringBuilder result = new StringBuilder();

            if (scene == null)
                return "NULL";

            result.AppendFormat("Scene at {0:X8}", scene.VirtualAddress.Start);
            result.AppendLine();
            result.Append(ReadHeader(scene.Header));

            return result.ToString();
        }

        public static string ReadRoom(Room room)
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Room at {0:X8}",
                room.VirtualAddress.Start);
            result.AppendLine();

            result.Append(ReadHeader(room.Header));
            return result.ToString();
        }

        private static string ReadHeader(SceneHeader Header)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Setup 0: 00000000");
            result.AppendLine(Header.Read());
            if (Header.HasAlternateHeaders())
            {
                var Alternate = Header.Alternate;
                for (int i = 0; i < Alternate.HeaderList.Count; i++)
                {
                    if (Alternate.HeaderList[i] == null)
                        continue;

                    if (i < 3)
                    {
                        result.AppendFormat("Setup {0}: ", (i + 1));
                    }
                    else
                    {
                        result.AppendFormat("Cutscene {0}: ", (i - 3));
                    }
                    result.AppendLine(Alternate.HeaderOffsetsList[i].ToString("X8"));
                    result.AppendLine(Alternate.HeaderList[i].Read());
                }
            }
            return result.ToString();
        }

        public static string GetEntranceCount(FileStream sr)
        {
            List<byte[]> list = new List<byte[]>();
            byte[] word = new byte[4];
            int foundIndex;
            StringBuilder result = new StringBuilder();

            sr.Seek(Addresser.GetRom(ORom.FileList.code, ORom.Build.DBGMQ, "EntranceIndexTable_Start"),
                SeekOrigin.Begin);
            for (int i = 0; i < 0x614; i++)
            {
                sr.Read(word, 0, 4);
                foundIndex = list.FindIndex(item => item[0] == word[0]);
                if (foundIndex == -1)
                {
                    list.Add(new byte[] { word[0], word[1] });
                }
                else if (list[foundIndex][1] < word[1])
                {
                    list[foundIndex][1] = word[1];
                }
            }
            foreach (byte[] item in list)
            {
                result.AppendFormat("{0}, {1}", item[0], (item[1] + 1));
                result.AppendLine();
            }
            return result.ToString();
        }

        private static void AppendActorList(List<List<ActorRecord>> actorList, int scene, int room, StringBuilder result)
        {
            List<ActorRecord> setupList;
            string locationStr;
            if (actorList.Count > 0)
            {
                for (int setup = 0; setup < actorList.Count; setup++)
                {
                    locationStr = string.Format("{0},{1},{2},",
                        scene,
                        setup,
                        room);
                    setupList = actorList[setup];
                    foreach (ActorRecord actor in setupList)
                    {
                        result.AppendLine(locationStr + actor.PrintCommaDelimited());
                    }
                }
            }
        }

        private static void AppendObjectList(List<List<ushort>> objectList, int scene, int room, StringBuilder result)
        {
            List<ushort> setupList;
            string locationStr;
            if (objectList.Count > 0)
            {
                for (int setup = 0; setup < objectList.Count; setup++)
                {
                    setupList = objectList[setup];
                    var list = setupList.ConvertAll(new Converter<ushort, string>((x) => { return x.ToString("X4"); }));

                    if (setupList.Count == 0)
                        continue;
                    locationStr = string.Format("{0:D3},{1:D2},{2:D2},",
                        scene,
                        setup,
                        room);
                    result.AppendLine(locationStr + String.Join(" ", list));

                    //foreach (ushort obj in setupList)
                    //{
                    //    result.AppendLine(locationStr + obj.ToString("X4"));
                    //}
                }
            }
        }

        delegate int PrintList(int id, SceneHeader header, int sceneId, int roomId, StringBuilder result);

        private static string PrintCollectionById(Rom rom, int id, PrintList PrintListFunction)
        {
            Scene scene;
            List<FileAddress> roomAddresses;
            Room room;
            int sceneId;
            int roomId;
            StringBuilder result;
            int count = 0;

            result = new StringBuilder();
            result.AppendLine("Scene,Setup,Room");
            for (sceneId = 0; sceneId < rom.SceneCount; sceneId++)
            {
                //set room negative to denote scene level actors
                roomId = -1;

                //load scene
                scene = InitializeScene(sceneId, rom.Files.GetSceneFile(sceneId));
                if (scene == null)
                    continue;

                //scene level actors
                count += PrintListFunction(id, scene.Header, sceneId, roomId, result);

                //load room list
                roomAddresses = scene.Header.GetRoomAddresses();

                for (roomId = 0; roomId < roomAddresses.Count; roomId++)
                {
                    try
                    {
                        room = InitializeRoom(rom.Files.GetFile(roomAddresses[roomId]));
                        count += PrintListFunction(id, room.Header, sceneId, roomId, result);
                    }
                    catch { }
                }
            }
            result.AppendLine("Total: " + count.ToString());
            return result.ToString();
        }

        private static int ObjectListPrintout(int id, SceneHeader header, int sceneId, int roomId, StringBuilder result)
        {
            List<List<ushort>> objectList = header.GetObjectsWithId(id);
            AppendObjectList(objectList, sceneId, roomId, result);
            return GetSublistCount(objectList);
        }

        private static int ActorListPrintout(int id, SceneHeader header, int sceneId, int roomId, StringBuilder result)
        {
            List<List<ActorRecord>> actorList = header.GetActorsWithId(id);
            AppendActorList(actorList, sceneId, roomId, result);
            return GetSublistCount(actorList);
        }

        private static int CommandListPrintout(int id, SceneHeader header, int sceneId, int roomId, StringBuilder result)
        {
            List<List<SceneCommand>> commandList = header.GetAllCommandsWithId(id);
            AppendCommandList(commandList, sceneId, roomId, result);
            return GetSublistCount(commandList);
        }

        public static string GetObjectsById(Rom r, int id)
        {
            return PrintCollectionById(r, id, ObjectListPrintout);
        }

        public static string GetActorsById(Rom r, int id)
        {
            return PrintCollectionById(r, id, ActorListPrintout);
        }

        public static string GetCommandsById(Rom r, int id)
        {
            return PrintCollectionById(r, id, CommandListPrintout);
        }

        public static int GetSublistCount<T>(List<List<T>> list)
        {
            int count = 0;
            foreach (List<T> sublist in list)
            {
                count += sublist.Count;
            }
            return count;
        }

        private static void AppendCommandList(List<List<SceneCommand>> commandList, int scene, int room, StringBuilder result)
        {
            List<SceneCommand> setupList;
            string locationStr;
            if (commandList.Count > 0)
            {
                for (int setup = 0; setup < commandList.Count; setup++)
                {
                    locationStr = string.Format("{0},{1},{2},",
                        scene,
                        setup,
                        room);
                    setupList = commandList[setup];
                    foreach (SceneCommand cmd in setupList)
                    {
                        result.AppendLine(locationStr + cmd.ReadSimple());
                    }
                }
            }
        }

        //public static void CommandOrderTest(BinaryReader br)
        //{
        //    byte[] commandArray = new byte[] { 0x18, 0x15, 0x04, 0x0E, 0x019, 0x03, 0x06, 0x07, 0x0D, 0x00, 0x01, 0x11, 0x13, 0x0F, 0x17, 0x14 };

    }
}