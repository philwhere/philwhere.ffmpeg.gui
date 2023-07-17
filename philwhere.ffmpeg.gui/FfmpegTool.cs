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
        private string _changedOutputDirectory;
        private string PrettyPrintFfmpegArguments => BuildPrettyFfmpegArgs();
        private string ActualFfmpegArguments => PrettyPrintFfmpegArguments.Replace(Environment.NewLine, string.Empty);
        private string AspectRatioArgument => 
            ignoreAspectRatioCheckBox.Checked ? null : $"-aspect {aspectRatioTextBox.Text} {Environment.NewLine}-c copy ";
        private string StereoDownmixArgument => ignoreDownmixCheckBox.Checked ? null : "-ac 1 ";
        private string DefaultCopyArgument => ignoreAspectRatioCheckBox.Checked && ignoreDownmixCheckBox.Checked ? "-c copy " : "";


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
            progressBar.MarqueeAnimationSpeed = 15;
            progressBar.Style = ProgressBarStyle.Marquee;
            process.Exited += (_, _) => Invoke(new Action(() =>
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = 100;
                UpdateEverything();
            }));
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

        private void dirTextBox_TextChanged(object sender, EventArgs e)
        {
            _changedOutputDirectory = dirTextBox.Text == _file.DirectoryName ? null : dirTextBox.Text;
            UpdateArgumentPreview();
        }


        private void UpdateEverything()
        {
            if (_file.Exists)
            {
                Controls.OfType<GroupBox>().ToList().ForEach(g => g.Enabled = true);
                aspectRatioTextBox.Enabled = !ignoreAspectRatioCheckBox.Checked;
                aspectRatioTextBox.Font = new Font(aspectRatioTextBox.Font,
                    ignoreAspectRatioCheckBox.Checked ? FontStyle.Strikeout : FontStyle.Regular);
                dirTextBox.Text = _changedOutputDirectory ?? _file.DirectoryName;
            }
            UpdateArgumentPreview();
            aspectRatioGroupBox.Enabled = ignoreDownmixCheckBox.Checked;
            audioGroupBox.Enabled = ignoreAspectRatioCheckBox.Checked;
            progressBar.Value = 0;
        }

        private void UpdateArgumentPreview()
        {
            cliTextBox.Text = prettyPrintCheckBox.Checked
                ? PrettyPrintFfmpegArguments
                : ActualFfmpegArguments;
        }

        private string BuildPrettyFfmpegArgs()
        {
            var name = Path.GetFileNameWithoutExtension(_file.FullName);
            var container = containerGroup.Controls.OfType<RadioButton>()
                .First(n => n.Checked).Text;

            var outputFilename = GetOutputFilename(name, container);
            return $"-i \"{_file.Name}\" " +
                   Environment.NewLine +
                   AspectRatioArgument +
                   StereoDownmixArgument +
                   DefaultCopyArgument +
                   $"\"{outputFilename}\"";
        }

        private string GetOutputFilename(string name, string container)
        {
            var cliOutputDirectory = dirTextBox.Text == _file.DirectoryName ? "" : dirTextBox.Text;
            string outputFilename;
            var incrementCounter = 0;
            do
            {
                outputFilename = AddToFilename(name, container, incrementCounter);
                incrementCounter++;
            } while (File.Exists(Path.Join(dirTextBox.Text, outputFilename)));

            return Path.Join(cliOutputDirectory, outputFilename);
        }

        private string AddToFilename(string name, string container, int incrementCounter)
        {
            var append = incrementCounter > 0 ? $"({incrementCounter})" : "";
            return $"{name}{append}.{container}";
        }
    }
}
