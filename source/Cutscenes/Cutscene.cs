using mzxrules.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib.Cutscenes
{
    public class Cutscene
    {
        int CommandCap;
        public List<CutsceneCommand> Commands = new List<CutsceneCommand>();
        public bool CommandCapReached = false;
        public bool InvalidCommandReached = false;
        public int Frames { get; set; }
        public int CommandCount { get { return Commands.Count; } }
        long Index;

        /// <summary>
        /// For reporting a parse error
        /// </summary>
        private int error_CommandCount;

        public Cutscene(int commandcap = 250)
        {
            CommandCap = commandcap;
        }

        public Cutscene(Stream s, int commandCap = 250)
        {
            BinaryReader br;
            CutsceneCommand cmd;
            Int32 commandId;
            int commands;

            CommandCap = commandCap;
            br = new BinaryReader(s);

            Index = br.BaseStream.Position;

            //Read the header
            commands = br.ReadBigInt32();
            Frames = br.ReadBigInt32();

            error_CommandCount = commands;

            //early termination
            if (Frames < 0 || commands < 0)
                return;

            if (commands > commandCap)
            {
                CommandCapReached = true;
                return;
            }

            for (int i = 0; i < commands + 1; i++)
            {
                cmd = null;
                var index = br.BaseStream.Position;
                commandId = br.ReadBigInt32();
                if (commandId > 0 && commandId < 0x91)
                {
                    switch ((byte)commandId)
                    {
                        case 0x01: cmd = new CameraCommand(commandId, br, index); break;
                        case 0x02: goto case 1;
                        case 0x05: goto case 1;
                        case 0x06: goto case 1;
                        case 0x09: cmd = new Command09(commandId, br, index); break;
                        case 0x13: cmd = new TextCommand(commandId, br, index); break;
                        case 0x2D: cmd = new ScreenTransitionCommand(commandId, br, index); break;
                        default: cmd = new ActorCommand(commandId, br, index); break;
                    }
                }
                else if (commandId == 0x3E8)
                    cmd = new ExitCommand(commandId, br, index);
                else if (commandId == -1)
                    cmd = new EndCommand(commandId, br, index);

                if (cmd != null)
                    Commands.Add(cmd);
                else
                {
                    InvalidCommandReached = true;
                    return;
                }

                if (cmd is EndCommand)
                    break;

                if (i > CommandCap)
                {
                    CommandCapReached = true;
                    return;
                }
            }
            TryMergeCameraCommands();
        }

        public bool TryMergeCameraCommands()
        {
            List<CameraCommand> FocusPoints; //0x02,0x06
            List<CameraCommand> CameraPositions; //0x01, 0x05

            for (int i = 0; i < 5; i += 4)
            {
                FocusPoints = Commands.OfType<CameraCommand>().Where(x => x.Command == 2+i).ToList();
                CameraPositions = Commands.OfType<CameraCommand>().Where(x => x.Command == 1+i).ToList();

                if (FocusPoints.Count != CameraPositions.Count
                    || !TryMergeCameraCommandLists(FocusPoints, CameraPositions))
                    return false;
            }
            return true;
        }

        private bool TryMergeCameraCommandLists(List<CameraCommand> FocusPoints, List<CameraCommand> CameraPositions)
        {
            for (int i = 0; i < FocusPoints.Count; i++)
            {
                if (FocusPoints[i].Entries.Count == CameraPositions[i].Entries.Count)
                    MergeCameraCommands(FocusPoints[i], CameraPositions[i]);
                else
                    return false;
            }
            return true;
        }

        private void MergeCameraCommands(CameraCommand focusPoints, CameraCommand cameraPositions)
        {
            for (int i = 0; i < focusPoints.Entries.Count; i++)
            {
                cameraPositions.Entries[i].StartFrame = focusPoints.Entries[i].StartFrame;
                cameraPositions.Entries[i].EndFrame = focusPoints.Entries[i].EndFrame;
            }
        }


        /// <summary>
        /// Generates a verbose dump of a cutscene, ordering instructions by occurrence within the file
        /// </summary>
        /// <returns>The output text</returns>
        public string PrintByOccurrence()
        {
            StringBuilder output = new StringBuilder();
            long streamStart;

            if (Commands.Count == 0)
                return "No Cutscene Found";

            if (InvalidCommandReached)
                return String.Format("Invalid Command");

            if (CommandCapReached)
                return String.Format("Exceeded {0} command limit: {1}", CommandCap, error_CommandCount);

            streamStart = Index;

            output.AppendLine(String.Format("Length: {0:X4} Hit End? {1}, Commands {2:X8}, End Frame {3:X8}",
                Commands.Sum(x => x.Length),
                (Commands.Exists(x => x is EndCommand)) ? "YES" : "NO",
                CommandCount,
                Frames));


            foreach (CutsceneCommand command in Commands)
            {
                output.AppendLine(String.Format("{0:X4}: {1}",
                    command.Index - streamStart,
                    command.ReadCommand()));
            }

            return output.ToString();
        }
        /// <summary>
        /// Generates a verbose dump of a cutscene, ordering intructions by start frame
        /// </summary>
        /// <returns>The output text</returns>
        public string PrintByTimeline()
        {
            short time = -1;
            StringBuilder sb = new StringBuilder();
            CutsceneCommand lastRoot = null;

            foreach (IFrameData f in Commands.OfType<IFrameCollection>()
                .SelectMany(x =>  x.IFrameDataEnum)
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

        public void Save(BinaryWriter bw)
        {
            bw.WriteBig(Commands.Count-1);
            bw.WriteBig(Frames);
            foreach (CutsceneCommand command in Commands)
                command.Save(bw);
        }
    }
}
