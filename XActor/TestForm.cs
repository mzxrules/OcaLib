using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace XActor
{
    public partial class TestForm : Form
    {
        static string XmlFileLocation = "ActorVars.xml";
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] Lines;
            XActors resultXActors;
            ParseOldFormat oldFormat = new ParseOldFormat();

            Lines = inRichTextBox.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            resultXActors = oldFormat.ParseLines(Lines);

            XmlSerializer serializer = new XmlSerializer(typeof(XActors));
            XmlWriterSettings xmlOutSettings = new XmlWriterSettings();
            xmlOutSettings.Indent = true;
            StringBuilder sb = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(sb, xmlOutSettings))
            {
                serializer.Serialize(writer, resultXActors);
            }
            firstConvertRichTextBox.Text = sb.ToString();

            outRichTextBox.Text = OutputNewFormat.Output(resultXActors).ToString();
        }

        private void TestGarbage()
        {
            string[] Lines;

            Lines = inRichTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


            Regex TestRegex = new Regex(""); //new Regex(IdLine + Comment);


            MatchCollection matches = TestRegex.Matches(Lines[0].Trim());

            firstConvertRichTextBox.Clear();

            foreach (Match m in matches)
            {
                //firstConvertRichTextBox.AppendText(m.ToString());
                //firstConvertRichTextBox.AppendText(Environment.NewLine);
                foreach (Group g in m.Groups)
                {
                    firstConvertRichTextBox.AppendText(g.Value);
                    firstConvertRichTextBox.AppendText(Environment.NewLine);
                }

            }

            //firstConvertRichTextBox.Text = IdLine.IsMatch(Lines[0].Trim()).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ActorObjectRelationshipsFromXml();
            PrintListFromXml();
        }

        private void ActorObjectRelationshipsFromXml()
        {
            XActors actorList;
            StringBuilder sb = new StringBuilder();
            List<Tuple<string, string>> ActorToObjects = new List<Tuple<string,string>>();

            actorList = XActors.LoadFromFile(XmlFileLocation);

            sb.AppendLine("{|class=\"wikitable sortable\"");
            sb.AppendLine("! data-sort-type=\"text\" | Actor");
            sb.AppendLine("! data-sort-type=\"text\" | Object");
            foreach (XActor actor in actorList.Actor)
            {
                foreach (string obj in actor.Objects.Object)
                {
                    if (obj != "????")
                    {
                        ActorToObjects.Add(new Tuple<string,string>(actor.id, obj));
                    }
                    else 
                    {
                        var objList = actor.Variables.SelectMany(v => v.Value)
                             .Select(x => x.Data.objectid)
                             .Where(y => !String.IsNullOrEmpty(y))
                             .Distinct();

                        foreach (string obj_sub in objList)
                        {
                            ActorToObjects.Add(new Tuple<string, string>(actor.id, obj_sub));
                        }
                    }
                }
            }
            foreach (Tuple<string, string> a2o in ActorToObjects)
            {
                sb.AppendLine("|-");
                sb.AppendFormat("|{0}||{1}", a2o.Item1, a2o.Item2);
                sb.AppendLine();
            }
            sb.AppendLine("|}");
            firstConvertRichTextBox.Text = sb.ToString();
        }


        private void PrintListFromXml()
        {
            outRichTextBox.Text = OutputNewFormat.Output(XActors.LoadFromFile(XmlFileLocation)).ToString();
        }
    }
}
