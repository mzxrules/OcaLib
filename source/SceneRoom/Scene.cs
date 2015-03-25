using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ZActor.OActors;
using RHelper;

namespace OcarinaPlayer.SceneRoom
{
    public class Scene : ISceneRoomHeader
    {
        /// <summary>
        /// Maximum number of entrances into an area
        /// </summary>
        public int MaxEntrances;
        /// <summary>
        /// Maximum number of exits from an area
        /// </summary>
        public int MaxExits;

        //public List<Map> Maps = new List<Map>();
        //public List<FileAddress> Maps = new List<FileAddress>();

        public int ID = -1;

        public Scene(Game game, int id, FileAddress virtualAddress)
        {
            Header = new SceneHeader(game);
            VirtualAddress = virtualAddress;
            ID = id;
        }
    }
}