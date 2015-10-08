using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.Cutscenes
{
    public interface IFrameData
    {
        CutsceneCommand RootCommand { get; set; }
        short StartFrame { get; set; }
        short EndFrame { get; set; }
    }
}
