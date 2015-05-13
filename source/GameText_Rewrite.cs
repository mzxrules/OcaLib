using mzxrules.OcaLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mzxrules.OcaLib
{
    partial class GameText
    {
        public class DialogWriterSettings
        {
            public ORom.Language Language { get; private set; }
            public OutputType OutputStyle { get; private set; }
            public bool VerboseMode = true;

            public enum OutputType
            {
                CsvTable,
                Wiki,
                Textbox
            }

            public DialogWriterSettings(ORom.Language lang, OutputType style, bool verbose = true)
            {
                Language = lang;
                OutputStyle = style;
                VerboseMode = verbose;
            }
        }

        public interface IDialogItem
        {
            string Print(DialogWriterSettings settings);
        }

        class Dialog
        {
            private StringBuilder Text;
            List<IDialogItem> dialogItems = new List<IDialogItem>();

            public Dialog()
            {
                Text = new StringBuilder();
            }

            public void Append(IDialogItem item)
            {
                if (Text.Length > 0)
                {
                    dialogItems.Add(new TextNode(Text.ToString()));
                    Text.Clear();
                }
                dialogItems.Add(item);
            }
            public void Append(String text)
            {
                Text.Append(text);
            }
            public void Append(char p)
            {
                Text.Append(p);
            }

            public void Complete()
            {
                if (Text.Length > 0)
                {
                    dialogItems.Add(new TextNode(Text.ToString()));
                    Text.Clear();
                }
            }

            internal string Print(DialogWriterSettings settings)
            {
                StringBuilder sb = new StringBuilder();
                foreach (IDialogItem item in dialogItems)
                {
                    sb.Append(item.Print(settings));
                }
                return sb.ToString();
            }

        }

        class TextNode : IDialogItem
        {
            private string text;

            public TextNode(string p)
            {
                // TODO: Complete member initialization
                this.text = p;
            }

            public string Print(DialogWriterSettings settings)
            {
                return text;
            }
        }
        /// <summary>
        /// 0x02
        /// </summary>
        class LineBreak : IDialogItem
        {
            public string Print(DialogWriterSettings settings)
            {
                switch (settings.OutputStyle)
                {
                    case DialogWriterSettings.OutputType.Wiki: return "<br>";
                    default: return Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 0x04
        /// </summary>
        class BoxBreak : IDialogItem
        {
            public string Print(DialogWriterSettings settings)
            {
                switch (settings.OutputStyle)
                {
                    case DialogWriterSettings.OutputType.Wiki: return "<br><br>";
                    default: return Environment.NewLine + Environment.NewLine;
                }

            }
        }

        class CustomCode : IDialogItem
        {
            public byte Id { get; private set; }
            int? parameter = null;

            public CustomCode(byte id, int? p = null)
            {
                Id = id;
                parameter = p;
            }

            public string Print(DialogWriterSettings settings)
            {
                if (settings.VerboseMode)
                {
                    if (parameter != null)
                        return String.Format("[{0:X2}:{1:X6}]", Id, parameter);
                    else
                        return String.Format("[{0:X2}]", Id);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        class GotoDialog : IDialogItem
        {
            public ushort Dialog { get; private set; }
            public GotoDialog(ushort d)
            {
                Dialog = d;
            }
            public string Print(DialogWriterSettings settings)
            {
                return String.Format("[goto {0:X4}]", Dialog);
            }
        }

        /// <summary>
        /// 0x05
        /// </summary>
        class SetTextColor: IDialogItem
        {
            public enum ColorCode
            {
                White = 0,
                Red = 1,
                Green = 2,
                Blue = 3,
                Cyan = 4,
                Pink = 5,
                Yellow = 6,
                Black = 7,
                Undefined
            }
            public ColorCode Color { get; private set; }
            private byte colorId;

            public SetTextColor(byte value)
            {
                colorId = value;
                if (0 <= colorId && colorId < 7)
                    Color = (SetTextColor.ColorCode)colorId;
                else
                    Color = ColorCode.Undefined;

            }

            public string Print(DialogWriterSettings settings)
            {
                string code;
                if (Color == ColorCode.Undefined)
                {
                    code = " " + colorId.ToString("X2");
                }
                else
                {
                    code = Color.ToString().Substring(0, 3);
                }
                return string.Format("[c:{0}]", code);
            }
        }

        /// <summary>
        /// 0x06
        /// </summary>
        class Padding : IDialogItem
        {
            public byte Spaces { get; private set; }
            public Padding(byte b)
            {
                Spaces = b;
            }
            public string Print(DialogWriterSettings settings)
            {
                //switch (settings.OutputStyle)
                //{
                //    case DialogWriterSettings.OutputType.Wiki:
                //        {
                //            string r = string.Empty;
                //            for (int i = 0; i < Spaces; i++)
                //            {
                //                r += "&nbsp;";
                //            }
                //            return r;
                //        }
                //    default: return String.Empty.PadRight(Spaces/2);

                //}
                return string.Format("[Pad:{0:X4}]", Spaces);
            }
        }

        /// <summary>
        /// 0x12
        /// </summary>
        class Sound : IDialogItem
        {
            public ushort SfxId {get; private set;}
            public Sound(ushort id)
            {
                SfxId = id;
            }
            public string Print(DialogWriterSettings settings)
            {
                string desc;
                switch (SfxId)
                {
                    case 0x0858: desc = "item fanfare"; break;
                    case 0x28E3: desc = "frog ribbit sound"; break;
                    case 0x28E4: desc = "frog ribbit sound"; break;
                    case 0x3880: desc = "deku squeak"; break;
                    case 0x3882: desc = "deku cry"; break;
                    case 0x38EC: desc = "Generic event"; break;
                    case 0x4807: desc = "Poe vanishing"; break;
                    case 0x486F: desc = "Twinrova"; break;
                    case 0x5965: desc = "Twinrova"; break;
                    case 0x6844: desc = "Navi hello"; break;
                    case 0x6852: desc = "Talon Ehh?"; break;
                    case 0x6855: desc = "Carpenter WAAAAA!"; break;
                    case 0x685F: desc = "Navi HEY!"; break;
                    case 0x6863: desc = "Saria giggle"; break;
                    case 0x6867: desc = "YAAAAAAA!"; break;
                    case 0x6869: desc = "Zelda heh"; break;
                    case 0x686B: desc = "Zelda awww"; break;
                    case 0x686C: desc = "Zelda huh"; break;
                    case 0x686D: desc = "Generic giggle"; break;
                    case 0x6864: desc = "??? used"; break;
                    default: desc = "Unknown"; break;
                }
                return string.Format("[sfx {0:X4}:{1}]", SfxId, desc);
            }
        }

        class ItemIcon : IDialogItem
        {
            public byte Item { get; private set; }
            public ItemIcon(byte item)
            {
                Item = item;
            }
            public string Print(DialogWriterSettings settings)
            {
                return string.Format("[Item Icon {0:X2}]", Item);
            }
        }

        public static string Rewrite_NesEscapeCustomCodes(byte[] s, DialogWriterSettings settings)
        {
            Dialog result = new Dialog();

            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    //[00]		---
                    case 0x00: break;
                    //[01]		line break
                    case 0x01:
                        result.Append(new LineBreak());
                        break;
                    //[02]		end marker
                    case 0x02: result.Complete(); i = s.Length; break;
                    //[03]		---
                    //[04]		wait for keypress / box break
                    case 0x04:
                        result.Append(new BoxBreak());
                        break;
                    //[05 xx]		use text color xx
                    case 0x05: i++;
                        result.Append(new SetTextColor((byte)(s[i] & 0x0F)));
                        break;
                    //  xx: 40 = white
                    //      41 = red
                    //      42 = green
                    //      43 = blue
                    //      44 = light blue
                    //      45 = pink
                    //      46 = yellow
                    //      47 = black
                    //[06 xx]		print xx spaces
                    case 0x06: i++;
                        result.Append(new Padding(s[i]));
                        break;
                    //[07 xx xx]	continue with message with id xxxx
                    case 0x07: i++; result.Append(new GotoDialog(Endian.ConvertUShort(s, i))); i++; break;
                    //.Text.AppendFormat("[goto {0:X4}]", Endian.ConvertUShort(s, i)); i++; 
                    //[08]		pring following text instantly
                    case 0x08: result.Append(new CustomCode(0x08)); break;
                    //[09]		disable 08, instant text printing
                    case 0x09: result.Append(new CustomCode(0x09)); break;
                    //[0a]		keep box opened, no reaction to keypresses (used in shop item descriptions)
                    case 0x0A: result.Append(new CustomCode(0x0A));  break;
                    //[0b]		(wait for external action? used in minigame texts)
                    case 0x0B: result.Append(new CustomCode(0x0B)); break;
                    //[0c xx]		delay text printing by xx
                    case 0x0C: i++; result.Append(new CustomCode(0x0C, s[i])); break;
                    //.Text.AppendFormat("[0C:{0:X2}]", s[i]); break;
                    //[0d]		wait for keypress / continue in same box/line
                    case 0x0D: result.Append(new CustomCode(0x0D)); break;
                    //[0e xx]		fade out interface and wait until xx maxes out(??), ignore all following text
                    case 0x0E: i++; result.Append(new CustomCode(0x0E, s[i])); break;
                    //result.Text.AppendFormat("[0E:{0:X2}]", s[i]); break;
                    //[0f]		show player name
                    case 0x0F: result.Append("[Link]"); break;
                    //[10]		init ocarina playing
                    case 0x10: result.Append(new CustomCode(0x10)); break;
                    //[11]		(fade out interface and wait, ignore all following text, no parameters?)
                    case 0x11: result.Append(new CustomCode(0x11)); break;
                    //[12 xx xx]	play sound xx xx
                    case 0x12: i++; result.Append(new Sound(Endian.ConvertUShort(s, i))); i++; break;
                    //  xx xx: 0858 item fanfare
                    //         28E3 frog ribbit sound
                    //         28E4 frog ribbit sound
                    //         3880 deku squeak
                    //         3882 deku cry
                    //         38EC Generic event
                    //         4807 Poe vanishing
                    //         486F Twinrova
                    //         5965 Twinrova
                    //         6844 Navi hello
                    //         6852 Talon Ehh?
                    //         6855 Carpenter WAAAAA!
                    //         685F Navi HEY!
                    //         6863 Saria giggle
                    //         6867 YAAAAAAA!
                    //         6869 Zelda heh
                    //         686B Zelda awww
                    //         686C Zelda huh
                    //         686D Generic giggle
                    //         6864 ??? used
                    //         (values documented by OriginalLink)
                    //[13 xx]		show item icon xx
                    case 0x13: i++; result.Append(new ItemIcon(s[i])); break;
                        //result.Text.AppendFormat("", s[i]);
                    //  xx: 00 = deku stick
                    //      01 = deku nut
                    //      ...
                    //[14 xx]		delay printing of each letter by xx
                    case 0x14: i++; result.Append(new CustomCode(0x14, s[i])); break;
                    //[15 xx yy zz]	load box background image (valid values unknown, 00 20 00 gives red 'X' mark)
                    case 0x15: i += 3; break;
                    //[16]		(show result time of something?)
                    case 0x16: break;
                    //[17]		(show result time of something else?)
                    case 0x17: break;
                    //[18]		(show result count of something?)
                    case 0x18: break;
                    //[19]		show golden skulltula count
                    case 0x19: result.Append("[Gold Skulltulas]"); break;
                    //[1a]		following text can't be skipped with B button
                    case 0x1A: break;
                    //[1b]		init two-choice answer selection (i.e. yes/no arrow)
                    case 0x1B: break;
                    //[1c]		init three-choice answer selection
                    case 0x1C: break;
                    //[1d]		(show result count of something?)
                    case 0x1D: break;
                    //[1e xx]		show minigame result xx
                    case 0x1E: i++; result.Append(DisplayValue(s[i])); break;
                    //  xx: 00 = horseback archery points
                    //      02 = largest fish caught
                    //      03 = horse race time
                    //      04 = marathon time
                    //      06 = dampé race time
                    //      ...?
                    //[1f]		show current hyrule time
                    case 0x1F: result.Append("[World Time]"); break;
                    default: result.Append(ConvertSpecialChars(s[i])); break;
                }
            }
            return result.Print(settings);
        }

        private static string Rewrite_JpnEscapeCustomCodes(byte[] data)
        {
            Dialog dialog = new Dialog();
            StringBuilder sb = new StringBuilder();

            Decoder shift_jis_dec;
            Encoding shift_jis_enc;

            UInt16[] chars = new ushort[data.Length / 2];
            char[] c = new Char[data.Length / 2];

            int bytesUsed, charsUsed;
            bool completed;

            shift_jis_enc = System.Text.UTF8Encoding.GetEncoding("shift-jis");
            shift_jis_dec = shift_jis_enc.GetDecoder();

            for (int i = 0; i < c.Length; i++)
                shift_jis_dec.Convert(data, i * 2, 2, c, i, 1, false, out bytesUsed, out charsUsed, out completed);

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = Endian.ConvertUShort(data, i * 2);
            }

            for (int i = 0; i < chars.Length; i++)
            {
                switch (chars[i])
                {
                    #region SpecialChars
                    case 0x839F: sb.Append(ConvertSpecialChars((byte)chars[i])); break;
                    case 0x83A0: goto case 0x839F;// "[B]";
                    case 0x83A1: goto case 0x839F;// "[C]";
                    case 0x83A2: goto case 0x839F;// "[L]";
                    case 0x83A3: goto case 0x839F;// "[R]";
                    case 0x83A4: goto case 0x839F;// "[Z]";
                    case 0x83A5: goto case 0x839F;// "[C-Up]";
                    case 0x83A6: goto case 0x839F;// "[C-Down]";
                    case 0x83A7: goto case 0x839F;// "[C-Left]";
                    case 0x83A8: goto case 0x839F;// "[C-Right]";
                    case 0x83A9: goto case 0x839F;// "[Triangle]";
                    case 0x83AA: goto case 0x839F;// "[Control Stick]";
                    case 0x83AB: goto case 0x839F;// "[D-Pad]";
                    #endregion

                    //[00]		---

                    //[01]		line break
                    case 0x000A:
                        dialog.Append(new LineBreak()); break;
                        //sb.Append("<br>"); break;
                        //sb.AppendLine(); break;

                    //[02]		end marker
                    case 0x8170: i = c.Length; dialog.Complete(); break;
                    //[03]		---
                    //[04]		wait for keypress / box break
                    case 0x81a5:
                        dialog.Append(new BoxBreak()); break;
                        //sb.AppendFormat("<br>{0}<br>", c[i]); break;
                        //sb.AppendLine(); sb.AppendLine(c[i].ToString()); break;
                    //[05 xx]		use text color xx
                    case 0x000B: i++; dialog.Append(new SetTextColor((byte)chars[i])); break;
                    //sb.Append(ColorCode((byte)chars[i])); break;
                    //  xx: 00 = white
                    //      01 = red
                    //      02 = green
                    //      03 = blue
                    //      04 = light blue
                    //      05 = pink
                    //      06 = yellow
                    //      07 = black
                    //[06 xx]		print xx spaces
                    case 0x86C7: i++; break;
                    //[07 xx xx]	continue with message with id xx xx (ex. 033a)
                    case 0x81CB: dialog.Append(new GotoDialog(chars[++i])); break;
                        //sb.AppendFormat("[{0} mesg {1:X4}]", c[i], chars[++i]); break;
                    //[08]		pring following text instantly
                    case 0x8189: break;
                    //[09]		disable 08, instant text printing
                    case 0x818a: break;
                    //[0a]		keep box opened, no reaction to keypresses (used in shop item descriptions)

                    //[0b]		(wait for external action? used in minigame texts)

                    //[0c xx]		delay text printing by xx

                    //[0d]		wait for keypress / continue in same box/line

                    //[0e xx]		fade out interface and wait until xx maxes out(??), ignore all following text

                    //[0f]		show player name
                    case 0x874F: dialog.Append("[Link]"); break;
                    //[10]		init ocarina playing
                    //case 0x10: i++; break;
                    //[11]		(fade out interface and wait, ignore all following text, no parameters?)
                    //case 0x11: i++; break;
                    //[12 xx xx]	play sound xx xx
                    case 0x81F3: i++; dialog.Append(new Sound(chars[i])); break;
                    //sb.AppendFormat("[sound {0:X4}]", chars[i]); break;
                    //  xx xx: 0858 item fanfare
                    //         28E3 frog ribbit sound
                    //         28E4 frog ribbit sound
                    //         3880 deku squeak
                    //         3882 deku cry
                    //         38EC Generic event
                    //         4807 Poe vanishing
                    //         486F Twinrova
                    //         5965 Twinrova
                    //         6844 Navi hello
                    //         6852 Talon Ehh?
                    //         6855 Carpenter WAAAAA!
                    //         685F Navi HEY!
                    //         6863 Saria giggle
                    //         6867 YAAAAAAA!
                    //         6869 Zelda heh
                    //         686B Zelda awww
                    //         686C Zelda huh
                    //         686D Generic giggle
                    //         6864 ??? used
                    //         (values documented by OriginalLink)
                    //[13 xx]		show item icon xx
                    case 0x819A: i++; dialog.Append(new ItemIcon((byte)chars[i])); break;
                        //sb.AppendFormat("[Item Icon {0:X2}]", chars[i]); break;
                    //  xx: 00 = deku stick
                    //      01 = deku nut
                    //      ...
                    //[14 xx]		delay printing of each letter by xx
                    //case 0x14: i += 2; break;
                    //[15 xx yy zz]	load box background image (valid values unknown, 00 20 00 gives red 'X' mark)
                    //case 0x15: i += 4; break;
                    //[16]		(show result time of something?)
                    //case 0x16: i++; break;
                    //[17]		(show result time of something else?)
                    //case 0x17: i++; break;
                    //[18]		(show result count of something?)
                    //case 0x18: i++; break;
                    //[19]		show golden skulltula count
                    //case 0x19: i++; sb.Append("[Gold Skulltulas]"); break;
                    //[1a]		following text can't be skipped with B button
                    case 0x8199: break;
                    //[1b]		init two-choice answer selection (i.e. yes/no arrow)
                    //case 0x1B: i++; break;
                    //[1c]		init three-choice answer selection
                    //case 0x1C: i++; break;
                    //[1d]		(show result count of something?)
                    //case 0x1D: i++; break;
                    //[1e xx]		show minigame result xx
                    //case 0x1E: i += 2; break;
                    //  xx: 00 = horseback archery points
                    //      02 = largest fish caught
                    //      03 = horse race time
                    //      04 = marathon time
                    //      06 = dampé race time
                    //      ...?
                    //[1f]		show current hyrule time
                    //case 0x1F: i++; sb.Append("[World Time]"); break;
                    default:
                        dialog.Append(c[i]);
                        break;
                }
            }
            return sb.ToString();
        }

    }
}
