using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib
{
    public class MFileTable : VFileTable
    {
        public MFileTable(string romLoc, MRom.Build version)
        {
            using (FileStream fs = new FileStream(romLoc, FileMode.Open, FileAccess.Read))
            {
                dmaFile = new DmaData(fs, version);
            }

            this.romLoc = romLoc;
            this.Version = version;
        }

        #region GetFile

        public RomFile GetMessageFile(Rom.Language language)
        {
            switch (language)
            {
                case Rom.Language.Japanese: return GetFile(MRom.FileList.jpn_message_data_static);
                case Rom.Language.English: return GetFile(MRom.FileList.nes_message_data_static);
                case Rom.Language.German: return GetFile(MRom.FileList.ger_message_data_static);
                case Rom.Language.French: return GetFile(MRom.FileList.fra_message_data_static);
                default: throw new NotImplementedException();
            }
        }

        public RomFile GetSceneFile_old(int i)
        {
            FileAddress sceneFile = GetSceneVirtualAddress(i);
            if (sceneFile.Start != 0)
                return GetFile(sceneFile);
            return null;
        }
        public override RomFile GetSceneFile(int i)
        {
            byte? sceneIndex;
            sceneIndex = GetInternalSceneIndex(i);

            if (sceneIndex == null)
                return null;
            return GetFile(GetSceneVirtualAddress((sbyte)sceneIndex));
            //return GetFile(0x01F0D000);//GetSceneVirtualAddress(i));
        }

        /// <summary>
        /// Converts the scene index value stored in an entrance index into the internal scene number
        /// </summary>
        /// <param name="entranceSceneIndex"></param>
        /// <returns></returns>
        public byte? GetInternalSceneIndex(int entranceSceneIndex)
        {
            sbyte sceneIndex;
            int entranceTableBase;
            int entranceTableAddr;
            uint EntranceRecord;
            int entranceAddr;

            entranceTableBase = Addresser.GetRom(MRom.FileList.code, Version, "EntranceIndexTable_Start");
            entranceTableAddr = entranceTableBase + (sizeof(Int32) * 3) * entranceSceneIndex + 4;

            //Capture pointer
            if (!Addresser.TryGetRom(MRom.FileList.code, Version, (uint)ReadInt32(entranceTableAddr), out entranceAddr)
                || !Addresser.TryGetRom(MRom.FileList.code, Version, (uint)ReadInt32(entranceAddr), out entranceAddr))
            {
                return null;
            }
            EntranceRecord = (uint)ReadInt32(entranceAddr);

            sceneIndex = (sbyte)(EntranceRecord >> 24);
            sceneIndex = Math.Abs(sceneIndex);

            return (byte?)sceneIndex;
        }

        private Stream GetActorFile(int i)
        {
            return GetFile(GetActorVirtualAddress(i));
        }

        private Stream GetObjectFile(int i)
        {
            return GetFile(GetObjectVirtualAddress(i));
        }

        #endregion

        #region FetchAddresses

        public FileAddress GetActorVirtualAddress(int actor)
        {
            return GetFileByRomTable("ActorTable_Start", actor, 8 * sizeof(Int32));
        }

        public FileAddress GetObjectVirtualAddress(int obj)
        {
            if (obj == 0xE3
                || obj == 0xFB) //malformed object reference?
                return new FileAddress();

            return GetFileByRomTable("ObjectTable_Start", obj, 2 * sizeof(Int32));
        }

        public FileAddress GetSceneVirtualAddress(int scene)
        {
            return GetFileByRomTable("SceneTable_Start", scene, 4 * sizeof(Int32));
        }

        public FileAddress GetTitleCardVirtualAddress(int scene)
        {
            return GetFileByRomTable("SceneTable_Start", scene, 5 * sizeof(Int32), 2 * sizeof(Int32));
        }

        public FileAddress GetHyruleFieldSkyboxFile(int id)
        {
            return GetFileByRomTable("HyruleSkyboxTable_Start", id, 2 * sizeof(Int32));
        }

        public FileAddress GetOvl_EffectAddress(int index)
        {
            return GetFileByRomTable("ParticleTable_Start", index, 7 * sizeof(Int32));
        }

        private FileAddress GetFileByRomTable(string table, int index, int size, int offset = 0)
        {
            int valueAddr;
            int readValue;

            valueAddr = Addresser.GetRom(MRom.FileList.code, Version, table);
            valueAddr += index * size + offset;
            readValue = ReadInt32(valueAddr);
            return GetVRomAddress(readValue);
        }

        #endregion

        public long GetFirstSceneFile()
        {
            int startAddress;
            int sceneAddress;
            FileRecord fileRecord;
            List<long> addr = new List<long>();

            //if (version == MRom.Build.DBGMQ)
            //    throw new Exception();

            startAddress = Addresser.GetRom(MRom.FileList.code, Version, "SceneTable_Start");
            fileRecord = GetFileStart(startAddress);

            using (BinaryReader reader = new BinaryReader(GetFile(fileRecord)))
            {
                for (int scene = 0; scene < 0; scene++)
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
