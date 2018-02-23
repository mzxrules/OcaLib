using mzxrules.OcaLib.Actor;
using System.Collections.Generic;

namespace mzxrules.OcaLib.SceneRoom.Commands
{
    public interface IActorList
    {
        List<ActorRecord> GetActors();
    }
}
