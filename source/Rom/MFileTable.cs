using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mzxrules.Helper;

namespace mzxrules.OcaLib
{
    public class MFileTable : VFileTable
    {
        public MFileTable(string romLocation, RomVersion version)
        {
            using (FileStream fs = new FileStream(romLocation, FileMode.Open, FileAccess.Read))
            {
                DmaFile = new DmaData(fs, version);
            }
            RomLocation = romLocation;
            Version = version;
            SceneTable = new FileRefTable("SceneTable_Start", 0x10, 0);
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
                case Rom.Language.Spanish: return GetFile(MRom.FileList.spa_message_data_static);
                default: throw new NotImplementedException();
            }
        }
        
        public override RomFile GetSceneFile(int i)
        {
            FileAddress sceneFile;
            byte? sceneIndex;
            sceneIndex = GetInternalSceneIndex(i);

            if (sceneIndex == null)
                return null;

            sceneFile = GetSceneVirtualAddress((sbyte)sceneIndex);
            if (sceneFile.Start == 0)
                return null;
            return GetFile(sceneFile);
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

        #endregion

        #region FetchAddresses


        //public FileAddress GetTitleCardVirtualAddress(int scene)
        //{
        //    return GetFileByRomTable("SceneTable_Start", scene, 5 * sizeof(Int32), 2 * sizeof(Int32));
        //}

        //public FileAddress GetHyruleFieldSkyboxFile(int id)
        //{
        //    return GetFileByRomTable("HyruleSkyboxTable_Start", id, 2 * sizeof(Int32));
        //}
        #endregion
    }
}
