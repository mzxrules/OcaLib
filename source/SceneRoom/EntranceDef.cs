using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.ZActor.OActors;

namespace mzxrules.OcaLib.SceneRoom
{
    public struct EntranceDef
    {
        public byte Room;
        public byte Position;
        public EntranceDef(byte position, byte room)
        {
            Room = room;
            Position = position;
        }
    }
}