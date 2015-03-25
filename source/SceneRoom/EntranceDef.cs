using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZActor.OActors;

namespace OcarinaPlayer.SceneRoom
{
    public struct EntranceDef
    {
        public byte Map;
        public byte Position;
        public EntranceDef(byte pIndex, byte m)
        {
            Map = m;
            Position = pIndex;
        }
    }
}