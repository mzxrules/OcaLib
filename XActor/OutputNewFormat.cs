using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mzxrules.XActor
{
    static class OutputNewFormat
    {
        public static StringBuilder Output(XActors root)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("== Actors ==");
            sb.AppendLine("<pre>");
            foreach (XActor actor in root.Actor)
            {
                PrintActor(sb, actor);
                sb.AppendLine();
            }
            sb.AppendLine("</pre>");
            return sb;
        }

        private static void PrintActor(StringBuilder sb, XActor actor)
        {
            sb.AppendFormat("Actor: {0} {1}", actor.id, actor.Description);
            sb.AppendLine();
            sb.AppendFormat("Object{1}: {0}",
                string.Join(", ", actor.Objects.Object),
                (actor.Objects.Object.Count > 1) ? "s" : "");
            sb.AppendLine();
            PrintComments(sb, actor.Comment);
            if (actor.Variables.Count > 0)
                sb.AppendLine("Variable:");

            foreach (XVariable var in actor.Variables)
            {
                if (var.altvar !=  AltVarTypes.Var)
                {
                    sb.AppendLine();
                    if (var.altvar == AltVarTypes.XRot)
                    {
                        sb.AppendLine("X Rotation:");
                    }
                    else if (var.altvar == AltVarTypes.YRot)
                    {
                        sb.AppendLine("Y Rotation:");
                    }
                    else if (var.altvar == AltVarTypes.ZRot)
                    {
                        sb.AppendLine("Z Rotation:");
                    }
                    else
                        sb.AppendLine("//GENERR");
                }
                PrintVariable(sb, var);
            }
            if (!string.IsNullOrEmpty(actor.CommentOther))
            {
                sb.AppendLine();
                PrintComments(sb, actor.CommentOther);
            }
        }
        /// <summary>
        /// Prints the stats on a packed initialization variable
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="var"></param>
        private static void PrintVariable(StringBuilder sb,  XVariable var)
        {
            sb.AppendFormat(" {0} {1} - {2} ",
                (var.maskType == MaskType.And)? "&":"|",
                var.mask,
                var.Description);
            PrintComments(sb, var.Comment, true);
            sb.AppendLine();

            foreach (XVariableValue value in var.Value)
            {
                PrintVariableValue(sb, value, int.Parse(var.mask, System.Globalization.NumberStyles.HexNumber));
            }
        }

        private static void PrintVariableValue(StringBuilder sb, XVariableValue value, int mask)
        {
            string obj = null;
            if (value.Meta != null)
                obj = string.Join(", ", value.Meta.Object);
            sb.AppendFormat("  - {0}{1} [{2:X4}]{3} = {4}",
                value.Data,
                (!String.IsNullOrEmpty(value.repeat)) ? "+" : " ",
                Shift(int.Parse(value.Data, System.Globalization.NumberStyles.HexNumber), mask),
                (!String.IsNullOrEmpty(obj)) ? String.Format(" ({0})", obj) : "",
                value.Description);
            PrintComments(sb, value.Comment, true);
            sb.AppendLine();
        }

        private static int Shift(int p, int mask)
        {
            return p << GetShift(mask);
        }
        private static int GetShift(int mask)
        {
            int shift;

            //a mask of 0 isn't valid, but could be set in the xml file by mistake.
            if (mask == 0)
                return 0;

            //Check the right bit
            //If the right bit is 0 shift the mask over one and increment the shift count
            //If the right bit is 1, the right side of the mask is found, we know how much to shift by
            for (shift = 0; (mask & 1) == 0; mask >>= 1)
            {
                shift++;
            }

            return shift;
        }

        private static void PrintComments(StringBuilder sb, string p, bool inline = false)
        {
            string[] commentLines;
            bool emptyLine = false;
            bool firstLine = true;

            if (p == null)
                return;

            commentLines = p.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);

            if (commentLines.Length == 0)
                return;

            if (inline == true)
            {
                if (commentLines.Length == 1)
                {
                    sb.Append(@" //" + commentLines[0].Trim());
                    return;
                }
                else
                {
                    sb.AppendLine();
                }
            }

            for (int i = 0; i < commentLines.Length; i++)
            {
                string s = commentLines[i].Trim();

                if (s.Length == 0)
                {
                    emptyLine = true;
                    continue;
                }
                if (emptyLine && !firstLine)
                {
                    sb.AppendLine();
                }
                emptyLine = false;
                firstLine = false;
                sb.AppendLine(String.Format(@"//{0}",
                    s
                    ));
            }
        }
    }
}
