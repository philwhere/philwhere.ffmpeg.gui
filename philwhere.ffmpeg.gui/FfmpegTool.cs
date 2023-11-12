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
        private string FfmpegScript => BuildFfmpegScript();

        public FfmpegTool()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += Form_DragEnter;
            DragDrop += Form_DragDrop;
        }


        private void runButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dirTextBox.Text))
            {
                var createDirectory = MessageBox.Show(
                    $@"Directory ""{dirTextBox.Text}"" does not exist. Do you want to try creating it?",
                    @"Directory does not exist", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (createDirectory != DialogResult.Yes)
                    return;
                Directory.CreateDirectory(dirTextBox.Text);
            }

            RunCommandUsingCmd(FfmpegScript);
        }

        private void RunCommandUsingCmd(string script)
        {
            // & is how to run multiple commands
            var concatenatedCommand = script.Replace(Environment.NewLine, " & ");
            // /C carries out the command for cmd
            var argumentsForCmd = $"/C {concatenatedCommand}";
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = argumentsForCmd,
                    WorkingDirectory = _file.DirectoryName,
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = false
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

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                MessageBox.Show(@"Only one file is supported currently. You can run multiple instances.");
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
            UpdateScriptPreview();
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
            UpdateScriptPreview();
            aspectRatioGroupBox.Enabled = ignoreDownmixCheckBox.Checked;
            audioGroupBox.Enabled = ignoreAspectRatioCheckBox.Checked;
            progressBar.Value = 0;
        }

        private void UpdateScriptPreview()
        {
            cliTextBox.Text = FfmpegScript;
        }

        private string BuildFfmpegScript()
        {
            var inputFileName = $"\"{_file.Name}\"";
            var outputFilePath = $"\"{GetOutputPath()}\"";

            if (!ignoreDownmixCheckBox.Checked)
                return GetAudioStereoDownmixScript(inputFileName, outputFilePath);

            if (!ignoreAspectRatioCheckBox.Checked)
                return GetAspectRatioScript(inputFileName, outputFilePath);

            // just change container
            return $"ffmpeg.exe -i {inputFileName} -c copy {outputFilePath}";
        }

        private string GetAudioStereoDownmixScript(string inputFileName, string outputFilePath)
        {
            // using random GUID to name intermediate files that will be cleaned up
            // praying that using first 9 digits with suffix is unique enough
            var randomGuid = Guid.NewGuid().ToString("N")[..9];
            var temporaryVideoOnlyName = $"\"{randomGuid}_video.mkv\"";
            var temporaryAudioOnlyName = $"\"{randomGuid}_audio.mka\"";
            var temporaryAudioMonoName = $"\"{randomGuid}_audio_mono.mka\"";

            var script = $@"ffmpeg.exe -i {inputFileName} -map v -c copy {temporaryVideoOnlyName}
ffmpeg.exe -i {inputFileName} -map a -c copy -vn {temporaryAudioOnlyName}
ffmpeg.exe -i {temporaryAudioOnlyName} -ac 1 {temporaryAudioMonoName}
ffmpeg.exe -i {temporaryVideoOnlyName} -i {temporaryAudioMonoName} -c copy -map 0 -map 1 -shortest {outputFilePath}
del {temporaryAudioOnlyName}
del {temporaryAudioMonoName}
del {temporaryVideoOnlyName}";

            return script;
        }

        private string GetAspectRatioScript(string inputFileName, string outputFilePath)
        {
            return $"ffmpeg.exe -i {inputFileName} -aspect {aspectRatioTextBox.Text} -c copy {outputFilePath}";
        }

        private string GetOutputPath()
        {
            var container = containerGroup.Controls.OfType<RadioButton>()
                .First(n => n.Checked).Text;

            var filenameWithoutExtension = Path.GetFileNameWithoutExtension(_file.FullName);

            var cliOutputDirectory = dirTextBox.Text == _file.DirectoryName ? "" : dirTextBox.Text;
            string outputFilename;
            var incrementCounter = 0;
            do
            {
                outputFilename = AddToFilename(filenameWithoutExtension, container, incrementCounter);
                incrementCounter++;
            } while (File.Exists(Path.Join(dirTextBox.Text, outputFilename)));

            return Path.Join(cliOutputDirectory, outputFilename);
        }

        private string AddToFilename(string filenameWithoutExtension, string container, int incrementCounter)
        {
            var suffixForDuplicates = incrementCounter > 0 ? $"({incrementCounter})" : "";
            return $"{filenameWithoutExtension}{suffixForDuplicates}.{container}";
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(FfmpegScript);
        }
    }
}
