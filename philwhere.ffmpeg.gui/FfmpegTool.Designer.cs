
namespace philwhere.ffmpeg.gui
{
    partial class FfmpegTool
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.runButton = new System.Windows.Forms.Button();
            this.cliTextBox = new System.Windows.Forms.TextBox();
            this.aspectRatioGroupBox = new System.Windows.Forms.GroupBox();
            this.ignoreAspectRatioCheckBox = new System.Windows.Forms.CheckBox();
            this.aspectRatioTextBox = new System.Windows.Forms.TextBox();
            this.audioGroupBox = new System.Windows.Forms.GroupBox();
            this.ignoreDownmixCheckBox = new System.Windows.Forms.CheckBox();
            this.prettyPrintCheckBox = new System.Windows.Forms.CheckBox();
            this.cliPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.dirTextBox = new System.Windows.Forms.TextBox();
            this.directoryGroup = new System.Windows.Forms.GroupBox();
            this.aspectRatioGroupBox.SuspendLayout();
            this.audioGroupBox.SuspendLayout();
            this.cliPreviewGroupBox.SuspendLayout();
            this.directoryGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Enabled = false;
            this.runButton.Location = new System.Drawing.Point(487, 122);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // cliTextBox
            // 
            this.cliTextBox.Location = new System.Drawing.Point(6, 22);
            this.cliTextBox.Multiline = true;
            this.cliTextBox.Name = "cliTextBox";
            this.cliTextBox.ReadOnly = true;
            this.cliTextBox.Size = new System.Drawing.Size(556, 94);
            this.cliTextBox.TabIndex = 3;
            this.cliTextBox.TabStop = false;
            this.cliTextBox.Text = "Drag file into form to get started";
            // 
            // aspectRatioGroupBox
            // 
            this.aspectRatioGroupBox.Controls.Add(this.ignoreAspectRatioCheckBox);
            this.aspectRatioGroupBox.Controls.Add(this.aspectRatioTextBox);
            this.aspectRatioGroupBox.Enabled = false;
            this.aspectRatioGroupBox.Location = new System.Drawing.Point(46, 29);
            this.aspectRatioGroupBox.Name = "aspectRatioGroupBox";
            this.aspectRatioGroupBox.Size = new System.Drawing.Size(200, 61);
            this.aspectRatioGroupBox.TabIndex = 4;
            this.aspectRatioGroupBox.TabStop = false;
            this.aspectRatioGroupBox.Text = "Aspect Ratio";
            // 
            // ignoreAspectRatioCheckBox
            // 
            this.ignoreAspectRatioCheckBox.AutoSize = true;
            this.ignoreAspectRatioCheckBox.Location = new System.Drawing.Point(100, 25);
            this.ignoreAspectRatioCheckBox.Name = "ignoreAspectRatioCheckBox";
            this.ignoreAspectRatioCheckBox.Size = new System.Drawing.Size(60, 19);
            this.ignoreAspectRatioCheckBox.TabIndex = 1;
            this.ignoreAspectRatioCheckBox.Text = "Ignore";
            this.ignoreAspectRatioCheckBox.UseVisualStyleBackColor = true;
            this.ignoreAspectRatioCheckBox.CheckedChanged += new System.EventHandler(this.anything_Changed);
            // 
            // aspectRatioTextBox
            // 
            this.aspectRatioTextBox.Enabled = false;
            this.aspectRatioTextBox.Location = new System.Drawing.Point(7, 23);
            this.aspectRatioTextBox.Name = "aspectRatioTextBox";
            this.aspectRatioTextBox.Size = new System.Drawing.Size(68, 23);
            this.aspectRatioTextBox.TabIndex = 0;
            this.aspectRatioTextBox.Text = "16:9";
            this.aspectRatioTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.aspectRatioTextBox.TextChanged += new System.EventHandler(this.anything_Changed);
            // 
            // audioGroupBox
            // 
            this.audioGroupBox.Controls.Add(this.ignoreDownmixCheckBox);
            this.audioGroupBox.Enabled = false;
            this.audioGroupBox.Location = new System.Drawing.Point(252, 29);
            this.audioGroupBox.Name = "audioGroupBox";
            this.audioGroupBox.Size = new System.Drawing.Size(200, 61);
            this.audioGroupBox.TabIndex = 5;
            this.audioGroupBox.TabStop = false;
            this.audioGroupBox.Text = "Merge Audio Channels";
            // 
            // ignoreDownmixCheckBox
            // 
            this.ignoreDownmixCheckBox.AutoSize = true;
            this.ignoreDownmixCheckBox.Checked = true;
            this.ignoreDownmixCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreDownmixCheckBox.Location = new System.Drawing.Point(15, 25);
            this.ignoreDownmixCheckBox.Name = "ignoreDownmixCheckBox";
            this.ignoreDownmixCheckBox.Size = new System.Drawing.Size(60, 19);
            this.ignoreDownmixCheckBox.TabIndex = 2;
            this.ignoreDownmixCheckBox.Text = "Ignore";
            this.ignoreDownmixCheckBox.UseVisualStyleBackColor = true;
            this.ignoreDownmixCheckBox.CheckedChanged += new System.EventHandler(this.anything_Changed);
            // 
            // prettyPrintCheckBox
            // 
            this.prettyPrintCheckBox.AutoSize = true;
            this.prettyPrintCheckBox.Checked = true;
            this.prettyPrintCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prettyPrintCheckBox.Location = new System.Drawing.Point(6, 122);
            this.prettyPrintCheckBox.Name = "prettyPrintCheckBox";
            this.prettyPrintCheckBox.Size = new System.Drawing.Size(82, 19);
            this.prettyPrintCheckBox.TabIndex = 6;
            this.prettyPrintCheckBox.Text = "PrettyPrint";
            this.prettyPrintCheckBox.UseVisualStyleBackColor = true;
            this.prettyPrintCheckBox.CheckedChanged += new System.EventHandler(this.anything_Changed);
            // 
            // cliPreviewGroupBox
            // 
            this.cliPreviewGroupBox.Controls.Add(this.prettyPrintCheckBox);
            this.cliPreviewGroupBox.Controls.Add(this.cliTextBox);
            this.cliPreviewGroupBox.Controls.Add(this.runButton);
            this.cliPreviewGroupBox.Enabled = false;
            this.cliPreviewGroupBox.Location = new System.Drawing.Point(46, 143);
            this.cliPreviewGroupBox.Name = "cliPreviewGroupBox";
            this.cliPreviewGroupBox.Size = new System.Drawing.Size(568, 158);
            this.cliPreviewGroupBox.TabIndex = 7;
            this.cliPreviewGroupBox.TabStop = false;
            this.cliPreviewGroupBox.Text = "FFMPEG Arguments";
            // 
            // dirTextBox
            // 
            this.dirTextBox.Location = new System.Drawing.Point(6, 18);
            this.dirTextBox.Name = "dirTextBox";
            this.dirTextBox.Size = new System.Drawing.Size(556, 23);
            this.dirTextBox.TabIndex = 9;
            this.dirTextBox.TextChanged += new System.EventHandler(this.dirTextBox_TextChanged);
            // 
            // directoryGroup
            // 
            this.directoryGroup.Controls.Add(this.dirTextBox);
            this.directoryGroup.Enabled = false;
            this.directoryGroup.Location = new System.Drawing.Point(46, 93);
            this.directoryGroup.Name = "directoryGroup";
            this.directoryGroup.Size = new System.Drawing.Size(568, 47);
            this.directoryGroup.TabIndex = 9;
            this.directoryGroup.TabStop = false;
            this.directoryGroup.Text = "Output Directory";
            // 
            // FfmpegTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 336);
            this.Controls.Add(this.directoryGroup);
            this.Controls.Add(this.cliPreviewGroupBox);
            this.Controls.Add(this.audioGroupBox);
            this.Controls.Add(this.aspectRatioGroupBox);
            this.Name = "FfmpegTool";
            this.Text = "FFMPEG GUI";
            this.aspectRatioGroupBox.ResumeLayout(false);
            this.aspectRatioGroupBox.PerformLayout();
            this.audioGroupBox.ResumeLayout(false);
            this.audioGroupBox.PerformLayout();
            this.cliPreviewGroupBox.ResumeLayout(false);
            this.cliPreviewGroupBox.PerformLayout();
            this.directoryGroup.ResumeLayout(false);
            this.directoryGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox cliTextBox;
        private System.Windows.Forms.GroupBox aspectRatioGroupBox;
        private System.Windows.Forms.CheckBox ignoreAspectRatioCheckBox;
        private System.Windows.Forms.TextBox aspectRatioTextBox;
        private System.Windows.Forms.GroupBox audioGroupBox;
        private System.Windows.Forms.CheckBox ignoreDownmixCheckBox;
        private System.Windows.Forms.CheckBox prettyPrintCheckBox;
        private System.Windows.Forms.GroupBox cliPreviewGroupBox;
        private System.Windows.Forms.TextBox dirTextBox;
        private System.Windows.Forms.GroupBox directoryGroup;
    }
}

