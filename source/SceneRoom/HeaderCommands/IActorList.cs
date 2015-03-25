using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZActor.OActors;

namespace OcarinaPlayer.SceneRoom.Commands
{
    interface IActorList
    {
        List<ActorRecord> GetActors();
    }
}
