﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mzxrules.OcaLib.Helper;

namespace mzxrules.OcaLib.SceneRoom
{
    public class ISceneRoomHeader
    {
        public FileAddress VirtualAddress { get; set; }
        public SceneHeader Header;
    }
}
