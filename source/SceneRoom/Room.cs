using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OcarinaPlayer;
using RHelper;

namespace OcarinaPlayer.SceneRoom
{
    public class Room : ISceneRoomHeader
    {

        public Room(Game game, FileAddress a)
        {
            Header = new SceneHeader(game);
            VirtualAddress = a;
        }
    }
}
