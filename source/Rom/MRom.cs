﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcarinaPlayer
{
    public partial class MRom: Rom
    {
        public new MFileTable Files { get { return (MFileTable) base.Files; } }
        //public MRom.Build Version { get { return Files.Version; } }
        public GameText Text { get; set; }

        public MRom(string fileLocation, MRom.Build version)
            : base(fileLocation, version)
        {
            SceneCount = 0x6E;
        }

    }
}
