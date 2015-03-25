using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcarinaPlayer.Cutscenes
{
    interface IFrameData
    {
        AbstractCutsceneCommand RootCommand { get; set; }
        short StartFrame { get; set; }
        short EndFrame { get; set; }
    }
}
