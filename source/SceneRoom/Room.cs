using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.OcaLib;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom
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
