
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
            runButton = new System.Windows.Forms.Button();
            cliTextBox = new System.Windows.Forms.TextBox();
            aspectRatioGroupBox = new System.Windows.Forms.GroupBox();
            ignoreAspectRatioCheckBox = new System.Windows.Forms.CheckBox();
            aspectRatioTextBox = new System.Windows.Forms.TextBox();
            audioGroupBox = new System.Windows.Forms.GroupBox();
            ignoreDownmixCheckBox = new System.Windows.Forms.CheckBox();
            cliPreviewGroupBox = new System.Windows.Forms.GroupBox();
            copyButton = new System.Windows.Forms.Button();
            progressBar = new System.Windows.Forms.ProgressBar();
            dirTextBox = new System.Windows.Forms.TextBox();
            directoryGroup = new System.Windows.Forms.GroupBox();
            containerGroup = new System.Windows.Forms.GroupBox();
            mkvButton = new System.Windows.Forms.RadioButton();
            mp4Button = new System.Windows.Forms.RadioButton();
            aspectRatioGroupBox.SuspendLayout();
            audioGroupBox.SuspendLayout();
            cliPreviewGroupBox.SuspendLayout();
            directoryGroup.SuspendLayout();
            containerGroup.SuspendLayout();
            SuspendLayout();
            // 
            // runButton
            // 
            runButton.Location = new System.Drawing.Point(487, 122);
            runButton.Name = "runButton";
            runButton.Size = new System.Drawing.Size(75, 23);
            runButton.TabIndex = 4;
            runButton.Text = "Run";
            runButton.UseVisualStyleBackColor = true;
            runButton.Click += runButton_Click;
            // 
            // cliTextBox
            // 
            cliTextBox.Location = new System.Drawing.Point(6, 22);
            cliTextBox.Multiline = true;
            cliTextBox.Name = "cliTextBox";
            cliTextBox.ReadOnly = true;
            cliTextBox.Size = new System.Drawing.Size(556, 94);
            cliTextBox.TabIndex = 3;
            cliTextBox.TabStop = false;
            cliTextBox.Text = "Drag file into form to get started";
            // 
            // aspectRatioGroupBox
            // 
            aspectRatioGroupBox.Controls.Add(ignoreAspectRatioCheckBox);
            aspectRatioGroupBox.Controls.Add(aspectRatioTextBox);
            aspectRatioGroupBox.Enabled = false;
            aspectRatioGroupBox.Location = new System.Drawing.Point(46, 29);
            aspectRatioGroupBox.Name = "aspectRatioGroupBox";
            aspectRatioGroupBox.Size = new System.Drawing.Size(200, 61);
            aspectRatioGroupBox.TabIndex = 4;
            aspectRatioGroupBox.TabStop = false;
            aspectRatioGroupBox.Text = "Aspect Ratio";
            // 
            // ignoreAspectRatioCheckBox
            // 
            ignoreAspectRatioCheckBox.AutoSize = true;
            ignoreAspectRatioCheckBox.Checked = true;
            ignoreAspectRatioCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            ignoreAspectRatioCheckBox.Location = new System.Drawing.Point(100, 25);
            ignoreAspectRatioCheckBox.Name = "ignoreAspectRatioCheckBox";
            ignoreAspectRatioCheckBox.Size = new System.Drawing.Size(60, 19);
            ignoreAspectRatioCheckBox.TabIndex = 1;
            ignoreAspectRatioCheckBox.Text = "Ignore";
            ignoreAspectRatioCheckBox.UseVisualStyleBackColor = true;
            ignoreAspectRatioCheckBox.CheckedChanged += anything_Changed;
            // 
            // aspectRatioTextBox
            // 
            aspectRatioTextBox.Enabled = false;
            aspectRatioTextBox.Location = new System.Drawing.Point(7, 23);
            aspectRatioTextBox.Name = "aspectRatioTextBox";
            aspectRatioTextBox.Size = new System.Drawing.Size(68, 23);
            aspectRatioTextBox.TabIndex = 0;
            aspectRatioTextBox.Text = "16:9";
            aspectRatioTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            aspectRatioTextBox.TextChanged += anything_Changed;
            // 
            // audioGroupBox
            // 
            audioGroupBox.Controls.Add(ignoreDownmixCheckBox);
            audioGroupBox.Enabled = false;
            audioGroupBox.Location = new System.Drawing.Point(252, 29);
            audioGroupBox.Name = "audioGroupBox";
            audioGroupBox.Size = new System.Drawing.Size(200, 61);
            audioGroupBox.TabIndex = 5;
            audioGroupBox.TabStop = false;
            audioGroupBox.Text = "Merge Audio Channels";
            // 
            // ignoreDownmixCheckBox
            // 
            ignoreDownmixCheckBox.AutoSize = true;
            ignoreDownmixCheckBox.Checked = true;
            ignoreDownmixCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            ignoreDownmixCheckBox.Location = new System.Drawing.Point(15, 25);
            ignoreDownmixCheckBox.Name = "ignoreDownmixCheckBox";
            ignoreDownmixCheckBox.Size = new System.Drawing.Size(60, 19);
            ignoreDownmixCheckBox.TabIndex = 2;
            ignoreDownmixCheckBox.Text = "Ignore";
            ignoreDownmixCheckBox.UseVisualStyleBackColor = true;
            ignoreDownmixCheckBox.CheckedChanged += anything_Changed;
            // 
            // cliPreviewGroupBox
            // 
            cliPreviewGroupBox.Controls.Add(copyButton);
            cliPreviewGroupBox.Controls.Add(progressBar);
            cliPreviewGroupBox.Controls.Add(cliTextBox);
            cliPreviewGroupBox.Controls.Add(runButton);
            cliPreviewGroupBox.Enabled = false;
            cliPreviewGroupBox.Location = new System.Drawing.Point(46, 143);
            cliPreviewGroupBox.Name = "cliPreviewGroupBox";
            cliPreviewGroupBox.Size = new System.Drawing.Size(568, 153);
            cliPreviewGroupBox.TabIndex = 7;
            cliPreviewGroupBox.TabStop = false;
            cliPreviewGroupBox.Text = "FFMPEG Script";
            // 
            // copyButton
            // 
            copyButton.Location = new System.Drawing.Point(6, 122);
            copyButton.Name = "copyButton";
            copyButton.Size = new System.Drawing.Size(75, 23);
            copyButton.TabIndex = 8;
            copyButton.Text = "Copy";
            copyButton.UseVisualStyleBackColor = true;
            copyButton.Click += copyButton_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(96, 122);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(374, 23);
            progressBar.TabIndex = 7;
            // 
            // dirTextBox
            // 
            dirTextBox.Location = new System.Drawing.Point(6, 18);
            dirTextBox.Name = "dirTextBox";
            dirTextBox.Size = new System.Drawing.Size(556, 23);
            dirTextBox.TabIndex = 9;
            dirTextBox.TextChanged += dirTextBox_TextChanged;
            // 
            // directoryGroup
            // 
            directoryGroup.Controls.Add(dirTextBox);
            directoryGroup.Enabled = false;
            directoryGroup.Location = new System.Drawing.Point(46, 93);
            directoryGroup.Name = "directoryGroup";
            directoryGroup.Size = new System.Drawing.Size(568, 47);
            directoryGroup.TabIndex = 9;
            directoryGroup.TabStop = false;
            directoryGroup.Text = "Output Directory";
            // 
            // containerGroup
            // 
            containerGroup.Controls.Add(mkvButton);
            containerGroup.Controls.Add(mp4Button);
            containerGroup.Enabled = false;
            containerGroup.Location = new System.Drawing.Point(458, 29);
            containerGroup.Name = "containerGroup";
            containerGroup.Size = new System.Drawing.Size(156, 61);
            containerGroup.TabIndex = 10;
            containerGroup.TabStop = false;
            containerGroup.Text = "Container";
            // 
            // mkvButton
            // 
            mkvButton.AutoSize = true;
            mkvButton.Location = new System.Drawing.Point(84, 24);
            mkvButton.Name = "mkvButton";
            mkvButton.Size = new System.Drawing.Size(48, 19);
            mkvButton.TabIndex = 1;
            mkvButton.Text = "mkv";
            mkvButton.UseVisualStyleBackColor = true;
            mkvButton.CheckedChanged += anything_Changed;
            // 
            // mp4Button
            // 
            mp4Button.AutoSize = true;
            mp4Button.Checked = true;
            mp4Button.Location = new System.Drawing.Point(20, 24);
            mp4Button.Name = "mp4Button";
            mp4Button.Size = new System.Drawing.Size(49, 19);
            mp4Button.TabIndex = 0;
            mp4Button.TabStop = true;
            mp4Button.Text = "mp4";
            mp4Button.UseVisualStyleBackColor = true;
            mp4Button.CheckedChanged += anything_Changed;
            // 
            // FfmpegTool
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(664, 336);
            Controls.Add(containerGroup);
            Controls.Add(directoryGroup);
            Controls.Add(cliPreviewGroupBox);
            Controls.Add(audioGroupBox);
            Controls.Add(aspectRatioGroupBox);
            Name = "FfmpegTool";
            Text = "FFMPEG GUI";
            aspectRatioGroupBox.ResumeLayout(false);
            aspectRatioGroupBox.PerformLayout();
            audioGroupBox.ResumeLayout(false);
            audioGroupBox.PerformLayout();
            cliPreviewGroupBox.ResumeLayout(false);
            cliPreviewGroupBox.PerformLayout();
            directoryGroup.ResumeLayout(false);
            directoryGroup.PerformLayout();
            containerGroup.ResumeLayout(false);
            containerGroup.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox cliTextBox;
        private System.Windows.Forms.GroupBox aspectRatioGroupBox;
        private System.Windows.Forms.CheckBox ignoreAspectRatioCheckBox;
        private System.Windows.Forms.TextBox aspectRatioTextBox;
        private System.Windows.Forms.GroupBox audioGroupBox;
        private System.Windows.Forms.CheckBox ignoreDownmixCheckBox;
        private System.Windows.Forms.GroupBox cliPreviewGroupBox;
        private System.Windows.Forms.TextBox dirTextBox;
        private System.Windows.Forms.GroupBox directoryGroup;
        private System.Windows.Forms.GroupBox containerGroup;
        private System.Windows.Forms.RadioButton mkvButton;
        private System.Windows.Forms.RadioButton mp4Button;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button copyButton;
    }
}

