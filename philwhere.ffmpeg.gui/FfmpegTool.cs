using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace philwhere.ffmpeg.gui
{
    public partial class FfmpegTool : Form
    {
        private FileInfo _file;
        private string PrettyPrintFfmpegArguments => BuildPrettyFfmpegArgs();
        private string ActualFfmpegArguments => PrettyPrintFfmpegArguments.Replace(Environment.NewLine, string.Empty);
        private string AspectRatioArgument => 
            ignoreAspectRatioCheckBox.Checked ? null : $"-aspect {aspectRatioTextBox.Text} " + Environment.NewLine;
        private string StereoDownmixArgument =>
            ignoreDownmixCheckBox.Checked ? null : "-ac 1 " + Environment.NewLine;

        public FfmpegTool()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += Form_DragEnter;
            DragDrop += Form_DragDrop;
        }


        private void runButton_Click(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "ffmpeg.exe",
                    WorkingDirectory = _file.DirectoryName,
                    Arguments = ActualFfmpegArguments,
                    UseShellExecute = true
                }
            };
            process.Start();
            process.EnableRaisingEvents = true;
            process.Exited += (_, _) => MessageBox.Show("Done");
        }

        private void Form_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form_DragDrop(object sender, DragEventArgs e) {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                MessageBox.Show("1 file at a time.");
                return;
            }

            _file = new FileInfo(files.Single());
            TabIndex = 0;
            UpdateEverything();
        }

        private void anything_Changed(object sender, EventArgs e)
        {
            UpdateEverything();
        }


        private void UpdateEverything()
        {
            if (_file.Exists)
            {
                runButton.Enabled = true;
                aspectRatioGroupBox.Enabled = true;
                audioGroupBox.Enabled = true;
                cliPreviewGroupBox.Enabled = true;
                aspectRatioTextBox.Enabled = !ignoreAspectRatioCheckBox.Checked;
                aspectRatioTextBox.Font = new Font(aspectRatioTextBox.Font,
                    ignoreAspectRatioCheckBox.Checked ? FontStyle.Strikeout : FontStyle.Regular);
                dirLabel.Text = $"Directory: {_file.DirectoryName}";
            }
            cliTextBox.Text = prettyPrintCheckBox.Checked ? PrettyPrintFfmpegArguments : ActualFfmpegArguments;
        }

        private string BuildPrettyFfmpegArgs()
        {
            var outputFilename = $"{Path.GetFileNameWithoutExtension(_file.FullName)}.mp4";
            if (outputFilename.Equals(_file.Name, StringComparison.OrdinalIgnoreCase))
                outputFilename = $"{Path.GetFileNameWithoutExtension(_file.FullName)}_done.mp4";
            return $"-i \"{_file.Name}\" " + Environment.NewLine +
                   AspectRatioArgument +
                   StereoDownmixArgument +
                   $"-c copy \"{outputFilename}\"";
        }
    }
}
