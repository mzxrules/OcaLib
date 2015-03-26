using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZActor.OActors;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    interface IActorList
    {
        List<ActorRecord> GetActors();
    }
}
