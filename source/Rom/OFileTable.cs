using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib
{
    /// <summary>
    /// Provides a means of accessing files in a Zelda 64 Rom
    /// </summary>
    public class OFileTable : VFileTable
    {
        public OFileTable(string romLoc, ORom.Build version)
        {
            using (FileStream fs = new FileStream(romLoc, FileMode.Open, FileAccess.Read))
            {
                DmaFile = new DmaData(fs, version);
            }

            this.RomLocation = romLoc;
            this.Version = version;
        }

        #region GetFile
        /// <summary>
        /// Returns a message_data_static file (the game's text dialog) for a specific language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public RomFile GetMessageFile(Rom.Language language)
        {
            switch (language)
            {
                case Rom.Language.Japanese: return GetFile(ORom.FileList.jpn_message_data_static);
                case Rom.Language.English: return GetFile(ORom.FileList.nes_message_data_static);
                case Rom.Language.German: return GetFile(ORom.FileList.ger_message_data_static);
                case Rom.Language.French: return GetFile(ORom.FileList.fra_message_data_static);
                default: throw new NotImplementedException();
            }
        }

        public override RomFile GetSceneFile(int i)
        {
            if (Version == ORom.Build.DBGMQ && i < 110
                || i < 101)
            {
                return GetFile(GetSceneVirtualAddress(i));
            }
            else
                return null;
        }

        private Stream GetActorFile(int i)
        {
            return GetFile(GetActorVRomAddress(i));
        }

        private Stream GetObjectFile(int i)
        {
            return GetFile(GetObjectVRomAddress(i));
        }

        #endregion

        #region FetchAddresses

        public FileAddress GetActorVRomAddress(int actor)
        {
            return GetFileByRomTable("ActorTable_Start", actor, 0x20);
        }

        public FileAddress GetObjectVRomAddress(int obj)
        {
            if (obj == 0xE3
                || obj == 0xFB) //malformed object references
                return new FileAddress();

            return GetFileByRomTable("ObjectTable_Start", obj, 2 * sizeof(Int32));
        }

        public FileAddress GetSceneVirtualAddress(int scene)
        {
            return GetFileByRomTable("SceneTable_Start", scene, 5 * sizeof(Int32));
        }

        public FileAddress GetTitleCardVirtualAddress(int scene)
        {
            return GetFileByRomTable("SceneTable_Start", scene, 5 * sizeof(Int32), 2 * sizeof(Int32));
        }

        public FileAddress GetHyruleFieldSkyboxFile(int id)
        {
            return GetFileByRomTable("HyruleSkyboxTable_Start", id, 2 * sizeof(Int32));
        }

        public FileAddress GetParticleEffectAddress(int index)
        {
            return GetFileByRomTable("ParticleTable_Start", index, 7 * sizeof(Int32));
        }

        private FileAddress GetFileByRomTable(string table, int index, int size, int offset = 0)
        {
            int valueAddr;

            valueAddr = Addresser.GetRom(ORom.FileList.code, Version, table);
            valueAddr += index * size + offset;
            return GetVRomAddress(ReadInt32(valueAddr));
        }

        #endregion

        #region GetOverlayRecord
        public ActorOverlayRecord GetActorOverlayRecord(int actor)
        {
            int addr;

            RomFile code = GetFile(ORom.FileList.code);
            if (!Addresser.TryGetRom(ORom.FileList.code, Version, "ActorTable_Start", out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (actor * 0x20));
            return new ActorOverlayRecord(actor, new BinaryReader(code));
        }

        public GameContextRecord GetGameContextRecord(int index)
        {
            int addr;

            RomFile code = GetFile(ORom.FileList.code);
            if (!Addresser.TryGetRom(ORom.FileList.code, Version, "GameContextTable_Start", out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (index * 0x30));
            return new GameContextRecord(index, new BinaryReader(code));
        }

        public ParticleEffectOverlayRecord GetParticleEffectOverlayRecord(int index)
        {
            int addr;

            RomFile code = GetFile(ORom.FileList.code);
            if (!Addresser.TryGetRom(ORom.FileList.code, Version, "ParticleTable_Start", out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (index * 0x1C));
            return new ParticleEffectOverlayRecord(index, new BinaryReader(code));
        }

        public PlayPauseOverlayRecord GetPlayPauseOverlayRecord(int index)
        {
            int addr;

            RomFile code = GetFile(ORom.FileList.code);
            if (!Addresser.TryGetRom(ORom.FileList.code, Version, "PlayerPauseOverlayTable_Start", out addr))
            {
                return null;
            }
            code.Stream.Position = code.Record.GetRelativeAddress(addr + (index * 0x1C));
            return new PlayPauseOverlayRecord(index, new BinaryReader(code));
        }

        #endregion

        public long GetFirstSceneFile()
        {
            int startAddress;
            int sceneAddress;
            FileRecord fileRecord;
            List<long> addr = new List<long>();

            if (Version == ORom.Build.DBGMQ)
                throw new Exception();

            startAddress = Addresser.GetRom(ORom.FileList.code, Version, "SceneTable_Start");
            fileRecord = GetFileStart(startAddress);

            using (BinaryReader reader = new BinaryReader(GetFile(fileRecord)))
            {
                for (int scene = 0; scene < 101; scene++)
                {
                    reader.BaseStream.Position
                        = fileRecord.GetRelativeAddress((scene * sizeof(Int32) * 5) + startAddress);
                    sceneAddress = reader.ReadBigInt32();
                    addr.Add(sceneAddress);
                }
            }

            return addr.Min();
        }

    }
}