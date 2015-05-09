using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mzxrules.XActor.Gui
{
    public partial class ActorControl : UserControl
    {
        public XActors Document { get; set; }
        XActor Actor = null;
        public ActorControl()
        {
            InitializeComponent();
        }

        public void SetActor(int actorId)
        {
            //Gui.SelectControl test = new Gui.SelectControl();

            var actor = (from a in Document.Actor
                         where a.id == actorId.ToString("X4")
                         select a)
                         .SingleOrDefault();

            if (actor != null)
            {
                SetActor(actor);
            }
        }

        private void SetActor(XActor actor)
        {
            Actor = actor;

            this.flowLayoutPanel.Controls.Clear();

            actorLabel.Text = string.Format("Actor {0}: {1}", Actor.id, Actor.Description);
            objectsLabel.Text = string.Format("Objects: {0}", string.Join(", ", Actor.Objects.Object));

            Control commentLabel = this.CreateCommentControl(4);
            commentLabel.Text = SetComment(Actor.Comment, Actor.CommentOther);
            flowLayoutPanel.Controls.Add(commentLabel);
            
            foreach (var item in Actor.Variables) 
            {
                if (item.UI.name != UITypes.none)
                {
                    BaseControl control;

                    switch (item.UI.name)
                    {
                        case UITypes.select: control = new SelectControl(); break;
                        case UITypes.switchflag: control = new FlagsControl(); break;
                        case UITypes.collectflag: control = new FlagsControl(); break;
                        case UITypes.chestflag: control = new FlagsControl(); break;
                        default: control = new NumControl(); break;
                    }
                    control.Dock = DockStyle.Fill;
                    control.SetUi(item);
                    flowLayoutPanel.Controls.Add(control);
                }
                else
                {
                    //pass default value
                }
                if (item.Comment != null)
                {
                    Label comment = new Label();
                    comment.MinimumSize = new Size(350, 0);
                    comment.Text = item.Comment;
                    flowLayoutPanel.Controls.Add(comment);
                }
            }

            //test.SetUi(actor.Variables[0]);
            //actorPanel.Controls.Add(test);
        }

        private string SetComment(string comment, string commentOther = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Helper.TrimComment(Actor.Comment));

            if (Actor.CommentOther != null)
            {
                if (Actor.Comment != null)
                    sb.AppendLine();
                sb.Append(Helper.TrimComment(Actor.CommentOther));
            }
            return sb.ToString();
        }

        private Control CreateCommentControl(int lines)
        {
            if (lines < 3)
            {
                Label l = new Label();
                l.AutoSize = true;
                l.MinimumSize = new Size(390, 0);
                return l;
            }
            else
            {
                RichTextBox rtx = new RichTextBox();
                rtx.MinimumSize = new Size(350, 0);
                rtx.Size = new Size(flowLayoutPanel.Size.Width-30, 40);
                rtx.BackColor = System.Drawing.SystemColors.Control;
                rtx.BorderStyle = System.Windows.Forms.BorderStyle.None;
                return rtx;
            }
        }
    }
}
