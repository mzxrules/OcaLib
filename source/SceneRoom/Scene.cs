using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mzxrules.ZActor.OActors;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom
{
    public class Scene : ISceneRoomHeader
    {
        public int Id { get; private set; }

        public Scene(Game game, int id, FileAddress virtualAddress)
        {
            Header = new SceneHeader(game);
            VirtualAddress = virtualAddress;
            Id = id;
        }
    }
}