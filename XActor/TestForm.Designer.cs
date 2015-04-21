namespace mzxrules.XActor
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataInRichTextBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.outRichTextBox = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.uiTestButton = new System.Windows.Forms.Button();
            this.actorTextBox = new System.Windows.Forms.TextBox();
            this.actorControl1 = new mzxrules.XActor.Gui.ActorControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataInRichTextBox
            // 
            this.dataInRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataInRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.dataInRichTextBox.Name = "dataInRichTextBox";
            this.dataInRichTextBox.Size = new System.Drawing.Size(404, 330);
            this.dataInRichTextBox.TabIndex = 1;
            this.dataInRichTextBox.Text = "";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(716, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(454, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(418, 362);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataInRichTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(410, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.outRichTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(410, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Edit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // outRichTextBox
            // 
            this.outRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.outRichTextBox.Name = "outRichTextBox";
            this.outRichTextBox.Size = new System.Drawing.Size(404, 330);
            this.outRichTextBox.TabIndex = 0;
            this.outRichTextBox.Text = "";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(797, 386);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Test2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // uiTestButton
            // 
            this.uiTestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiTestButton.Location = new System.Drawing.Point(118, 389);
            this.uiTestButton.Name = "uiTestButton";
            this.uiTestButton.Size = new System.Drawing.Size(75, 23);
            this.uiTestButton.TabIndex = 7;
            this.uiTestButton.Text = "Test UI";
            this.uiTestButton.UseVisualStyleBackColor = true;
            this.uiTestButton.Click += new System.EventHandler(this.uiTestButton_Click);
            // 
            // actorTextBox
            // 
            this.actorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actorTextBox.Location = new System.Drawing.Point(12, 389);
            this.actorTextBox.Name = "actorTextBox";
            this.actorTextBox.Size = new System.Drawing.Size(100, 20);
            this.actorTextBox.TabIndex = 9;
            // 
            // actorControl1
            // 
            this.actorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.actorControl1.Document = null;
            this.actorControl1.Location = new System.Drawing.Point(12, 12);
            this.actorControl1.MinimumSize = new System.Drawing.Size(350, 0);
            this.actorControl1.Name = "actorControl1";
            this.actorControl1.Size = new System.Drawing.Size(436, 362);
            this.actorControl1.TabIndex = 8;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 421);
            this.Controls.Add(this.actorTextBox);
            this.Controls.Add(this.actorControl1);
            this.Controls.Add(this.uiTestButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox dataInRichTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox outRichTextBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button uiTestButton;
        private Gui.ActorControl actorControl1;
        private System.Windows.Forms.TextBox actorTextBox;
    }
}

