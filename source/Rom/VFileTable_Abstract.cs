using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RHelper;

namespace mzxrules.OcaLib
{
    public abstract class AbstractVFileTable 
    {
        public abstract RomFile GetSceneFile(int i);

        public abstract FileAddress GetDmaDataStart();
    }
}
