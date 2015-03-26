using RHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.Cutscenes
{
    public class Cutscene
    {
        const int COMMAND_CAP = 250;
        HeaderCommand header; 
        List<CutsceneCommand> Commands = new List<CutsceneCommand>();
        public bool CommandCapReached = false;
        public int Frames { get { return header.EndFrame; } }
        public int CommandCount { get { return header.Commands; } }

        public Cutscene(Stream s)
        {
            BinaryReader br;
            CutsceneCommand cmd;
            UInt32 commandId;

            br = new BinaryReader(s);

            //Read the header
            header = new HeaderCommand(br);
            Commands.Add(header);

            //early termination
            if (header.EndFrame < 0 || header.Commands < 0)
                return;

            for (int i = 0; i < header.Commands + 1; i++)
            {
                Endian.Convert(out commandId, br.ReadBytes(4));
                switch (commandId)
                {
                    case 0x00000001: cmd = new CameraCommand(commandId, br); break;
                    case 0x00000002: goto case 1;
                    case 0x00000005: goto case 1; // Scene 67
                    case 0x00000006: goto case 1; // Scene 67
                    case 0x00000009: cmd = new Command0009(commandId, br); break;
                    case 0x00000013: cmd = new TextCommand(commandId, br); break;
                    case 0x0000002D: cmd = new ScreenTransitionCommand(commandId, br); break;
                    case 0x000003E8: cmd = new ExitCommand(commandId, br); break;
                    case 0xFFFFFFFF: cmd = new EndCommand(commandId, br); break;
                    default: cmd = new ActorCommand(commandId, br); break;
                }
                Commands.Add(cmd);

                if (cmd is EndCommand)
                    break;

                if (i > COMMAND_CAP)
                {
                    CommandCapReached = true;
                    return;
                }
            }
        }


        public string PrintCutsceneByCommandOrder()
        {
            StringBuilder output = new StringBuilder();
            long streamStart;

            if (Commands.Count == 0 || header == null)
                return "No Cutscene Found";

            if (CommandCapReached)//header.Commands > COMMAND_CAP)
                return String.Format("Exceeded {0} command limit: {1}", COMMAND_CAP, header.Commands);

            streamStart = header.Index;

            output.AppendLine(String.Format("Length: {0:X4} Hit End? {1}",
                Commands.Sum(x => x.Length),
                (Commands.Exists(x => x is EndCommand)) ? "YES" : "NO"));

            foreach (CutsceneCommand command in Commands)
            {
                output.AppendLine(String.Format("{0:X4}: {1}",
                    command.Index - streamStart,
                    command.ReadCommand()));
            }

            return output.ToString();
        }

        public string PrintCutsceneByTimelineOrder()
        {
            short time = -1;
            StringBuilder sb = new StringBuilder();
            AbstractCutsceneCommand lastRoot = null;

            foreach (IFrameData f in Commands
                .SelectMany(x => x.IFrameDataEnum)
                .OrderBy(x => x.StartFrame))
            {
                if (f.StartFrame > time)
                {
                    time = f.StartFrame;
                }
                
                if (lastRoot == null || f.RootCommand != lastRoot)
                {
                       sb.AppendLine();
                    sb.AppendLine(String.Format("{0:X4} {1:X6}: {2}", time, f.RootCommand.Index, f.RootCommand));
                    lastRoot = f.RootCommand;
                }
                if ((f != f.RootCommand))
                    sb.AppendLine(String.Format("{0:X4} {1:X6}:   {2}", time, 0xFFFFFF, f));
            }
            return sb.ToString();
        }
    }
}
