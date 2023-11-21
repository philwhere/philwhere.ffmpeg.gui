using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoreLinq;

namespace philwhere.ffmpeg.gui
{
    public partial class FfmpegTool : Form
    {
        private FileInfo _droppedFile;
        private DirectoryInfo _droppedDirectory;
        private DirectoryInfo _originDirectory;
        private string _changedOutputDirectory;
        private string FfmpegScript => BuildFfmpegScript();

        public FfmpegTool()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += Form_DragEnter;
            DragDrop += Form_DragDrop;
        }


        private async void runButton_Click(object sender, EventArgs e)
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

            var script = FfmpegScript;
            // There is a limit for arguments or something.
            // I was way less than the documented 32,699 string size
            // when I was testing, so I don't know. This worked for me.
            var commands = script.Split(Environment.NewLine);
            const int batchSize = 10; 
            var batches = commands.Batch(batchSize);
            // I am intentionally not running multiple process in parallel
            // so that I can ensure any script I add runs in the order it was written.
            foreach (var batch in batches)
                await RunCommandUsingCmd(string.Join(Environment.NewLine, batch));
        }

        private async Task RunCommandUsingCmd(string script)
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
                    WorkingDirectory = _originDirectory.FullName,
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
            await process.WaitForExitAsync();
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            var droppedItems = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (droppedItems.Length > 1)
            {
                MessageBox.Show(@"Only one file or folder is supported currently. You can run multiple instances.");
                return;
            }

            var droppedItemPath = droppedItems.Single();
            var itemAttributes = File.GetAttributes(droppedItemPath);

            if (itemAttributes.HasFlag(FileAttributes.Directory))
            {
                _droppedDirectory = new DirectoryInfo(droppedItemPath);
                _originDirectory = _droppedDirectory;
                _droppedFile = null;
            }
            else
            {
                _droppedFile = new FileInfo(droppedItemPath);
                _originDirectory = new DirectoryInfo(_droppedFile.DirectoryName);
                _droppedDirectory = null;
            }

            TabIndex = 0;
            UpdateEverything();
        }

        private void anything_Changed(object sender, EventArgs e)
        {
            UpdateEverything();
        }

        private void dirTextBox_TextChanged(object sender, EventArgs e)
        {
            _changedOutputDirectory = dirTextBox.Text == (_droppedFile?.DirectoryName ?? _droppedDirectory?.FullName)
                ? null
                : dirTextBox.Text;
            UpdateScriptPreview();
        }


        private void UpdateEverything()
        {
            if (_droppedFile != null || _droppedDirectory != null)
            {
                if (_droppedFile?.Exists == true)
                {
                    Controls.OfType<GroupBox>().ToList().ForEach(g => g.Enabled = true);
                    aspectRatioTextBox.Enabled = !ignoreAspectRatioCheckBox.Checked;
                    aspectRatioTextBox.Font = new Font(aspectRatioTextBox.Font,
                        ignoreAspectRatioCheckBox.Checked ? FontStyle.Strikeout : FontStyle.Regular);
                    UpdateScriptPreview();
                    aspectRatioGroupBox.Enabled = ignoreDownmixCheckBox.Checked;
                    audioGroupBox.Enabled = ignoreAspectRatioCheckBox.Checked;
                }

                if (_droppedDirectory?.Exists == true)
                {
                    Controls.OfType<GroupBox>().ToList().ForEach(g => g.Enabled = false);
                    containerGroup.Enabled = true;
                    cliPreviewGroupBox.Enabled = true;
                }
                dirTextBox.Text = _changedOutputDirectory ?? _droppedFile?.DirectoryName ?? _droppedDirectory?.FullName;
            }
            progressBar.Value = 0;
        }

        private void UpdateScriptPreview()
        {
            cliTextBox.Text = FfmpegScript;
        }

        private string BuildFfmpegScript()
        {
            if (_droppedFile != null)
                return BuildFileScript();
            return BuildDirectoryScript();
        }

        private string BuildDirectoryScript()
        {
            var files = Directory.GetFiles(_droppedDirectory.FullName);
            // The next line is does not cover scenarios where two files with different extensions have the same name.
            // This is a bit hard to handle because the target container is the same.
            // Luckily FFMPEG has the Y/N prompt. I'm not fixing this edge case.
            var commands = files.Select(GetContainerChangeCommand); 
            var script = string.Join(Environment.NewLine, commands);
            return script;
        }

        private string GetContainerChangeCommand(string filePath)
        {
            var inputFileName = $"\"{filePath}\"";
            var outputFilePath = $"\"{GetOutputPath(filePath)}\"";
            return $"ffmpeg.exe -i {inputFileName} -c copy {outputFilePath}";
        }

        private string BuildFileScript()
        {
            var inputFileName = $"\"{_droppedFile.Name}\"";
            var outputFilePath = $"\"{GetOutputPath(_droppedFile.FullName)}\"";

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

        private string GetOutputPath(string filePath)
        {
            var container = containerGroup.Controls.OfType<RadioButton>()
                .First(n => n.Checked).Text;

            var filenameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

            var cliOutputDirectory = dirTextBox.Text == _originDirectory.FullName ? "" : dirTextBox.Text;
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
