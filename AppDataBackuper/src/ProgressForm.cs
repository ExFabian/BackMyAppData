using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDataBackuper
{
    public partial class ProgressForm : Form
    {
        BackgroundWorker backupWorker = new BackgroundWorker();
        BackgroundWorker restoreWorker = new BackgroundWorker();

        RunMode Mode;

        bool ignoreInaccesibleFiles = false;
        bool verboseLogs = false;

        bool autoOverwrite = false;

        String fileName = "";
        public ProgressForm()
        {
            InitializeComponent();
            InitializeBackgroundWorkers();

            Reset();
        }

        public void Reset(RunMode mode = RunMode.Backup, String name = "")
        {
            LogBox.Clear();
            ProgressBar.Value = 0;
            StartOperationButton.Enabled = true;
            CancelOperationButton.Enabled = true;

            Mode = mode;
            fileName = name;

            if (Mode == RunMode.Backup)
                this.Text = "Create AppData Backup";
            else
                this.Text = "Restore AppData Backup";

            SetSettingCheckboxes(); 
        }

        private void SetSettingCheckboxes()
        {
            CommonSetting1.Text = "Automatically ignore files in use";
            CommonSetting1ToolTip.SetToolTip(CommonSetting1, "Automatically skip any file that can't be backed up due to being in use or being otherwise inaccesible without asking for permission first.");

            CommonSetting2.Text = "Enable verbose logs";
            CommonSetting2ToolTip.SetToolTip(CommonSetting2, "Show more verbose logging information. Useful for debugging purposes but not really otherwise.");

            if (Mode == RunMode.Backup)
            {
                SettingsLabel.Text = "Backup Settings";
                SpecificSetting1.Visible = false;
            }
            else
            {
                SettingsLabel.Text = "Restore Settings";
                SpecificSetting1.Visible = true;
                SpecificSetting1.Text = "Automatically overwrite folders";
                SpecificSetting1ToolTip.SetToolTip(SpecificSetting1, "Automatically overwrite already existing folders with the backup without asking for permission first.");
            }
        }

        public void Log(String text)
        {
            LogBox.AppendText(text + Environment.NewLine);
        }

        public void LogNoScroll(String text)
        {
            LogBox.Text += text + Environment.NewLine;
        }

        public void UpdateBar(int value)
        {
            ProgressBar.Value = value;
        }

        // worker stuff
        // ------------------------------------------------------------
        private void InitializeBackgroundWorkers()
        {
            backupWorker.DoWork += backupWorker_DoWork!;
            backupWorker.RunWorkerCompleted += backupWorker_RunWorkerCompleted!;
            backupWorker.ProgressChanged += backupWorker_ProgressChanged!;

            backupWorker.WorkerReportsProgress = true;
            backupWorker.WorkerSupportsCancellation = true;

            restoreWorker.DoWork += restoreWorker_DoWork!;
            restoreWorker.RunWorkerCompleted += restoreWorker_RunWorkerCompleted!;
            restoreWorker.ProgressChanged += restoreWorker_ProgressChanged!;

            restoreWorker.WorkerReportsProgress = true;
            restoreWorker.WorkerSupportsCancellation = true;
        }

        private void backupWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;

            if (worker != null)
                DoBackup((String)e.Argument!, worker, e);

            if (backupWorker.CancellationPending)
                e.Cancel = true;
        }

        private void backupWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Utils.ShowErrorMsg(ErrorCode.BackupException, e.Error.Message);
            }
            else if (e.Cancelled)
            {
                Utils.ShowErrorMsg(ErrorCode.BackupCancelled);
            }
            else
            {
                Utils.ShowErrorMsg(ErrorCode.OK);
            }

            Close();
        }

        private void backupWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
                UpdateBar(e.ProgressPercentage);

            if (e.UserState == null) return;

            if (e.UserState.GetType() == typeof(String)) // logging
            {
                String? text = e.UserState as String;
                if (text != null)
                    Log(text);
            }
            else if (e.UserState.GetType() == typeof(bool)) // toggle cancel button enabled
            {
                CancelOperationButton.Enabled = (bool)e.UserState;
            }
        }

        private void restoreWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;

            if (worker != null)
                DoRestore((String)e.Argument!, worker, e);

            if (restoreWorker.CancellationPending)
                e.Cancel = true;
        }

        private void restoreWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Utils.ShowErrorMsg(ErrorCode.RestoreException, e.Error.Message);
            }
            else if (e.Cancelled)
            {
                Utils.ShowErrorMsg(ErrorCode.RestoreCancelled);
            }
            else
            {
                Utils.ShowErrorMsg(ErrorCode.OK);
            }

            Close();
        }

        private void restoreWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
                UpdateBar(e.ProgressPercentage);

            if (e.UserState == null) return;

            if (e.UserState.GetType() == typeof(String)) // logging
            {
                String? text = e.UserState as String;
                if (text != null)
                    Log(text);
            }
            else if (e.UserState.GetType() == typeof(bool)) // toggle cancel button enabled
            {
                CancelOperationButton.Enabled = (bool)e.UserState;
            }    
        }
        // ------------------------------------------------------------

        private void StartOperationButton_Click(object sender, EventArgs e)
        {
            LogBox.Clear();
            StartOperationButton.Enabled = false;
            CancelOperationButton.Enabled = true;

            // todo: maybe put all the settings into an container? may make my life slightly easier in the future
            CommonSetting1.Enabled = false;
            CommonSetting2.Enabled = false;
            SpecificSetting1.Enabled = false;

            ignoreInaccesibleFiles = CommonSetting1.Checked;
            verboseLogs = CommonSetting2.Checked;

            if (Mode == RunMode.Backup)
            {
                backupWorker.RunWorkerAsync(fileName);
            }
            else if (Mode == RunMode.Restore)
            {
                autoOverwrite = SpecificSetting1.Checked;

                restoreWorker.RunWorkerAsync(fileName);
            }
        }

        private void CancelOperationButton_Click(object sender, EventArgs e)
        {
            if(backupWorker.IsBusy)
            {
                backupWorker.CancelAsync();
                backupWorker.ReportProgress(-1, "Cancellation has been ordered, waiting for response...");
                backupWorker.ReportProgress(-1, false);
            }
            else if(restoreWorker.IsBusy)
            {
                restoreWorker.CancelAsync();
                restoreWorker.ReportProgress(-1, "Cancellation has been ordered, waiting for response...");
                backupWorker.ReportProgress(-1, false);
            }
            else
            {
                Close();
            }
        }

        private void DoBackup(String fileName, BackgroundWorker worker, DoWorkEventArgs e)
        {
            int folderIndex = 0;

            // create a temporary folder in which we will copy the files to be compressed later
            // todo: there might be a faster way to do this, maybe adding the folders to an archive one by one without needing to copy them over? research
            try
            {
                Directory.CreateDirectory(Global.TempPath);

                worker.ReportProgress(0, $"Created temp directory: {Global.TempPath}");

                String[] foldersToCreate = { @"Local\", @"Roaming\", @"LocalLow\" };

                foreach (CheckedItem item in Global.checkedList)
                {
                    String sourceDir = "";
                    String appDataFolder = "";
                    foreach (String folder in foldersToCreate)
                    {
                        if (item.Name.Contains(folder))
                        {
                            Directory.CreateDirectory(Path.Combine(Global.TempPath, folder));
                            sourceDir = item.Name;
                            appDataFolder = folder;
                            break;
                        }
                    }

                    if (sourceDir != "" && appDataFolder != "")
                    {
                        worker.ReportProgress(folderIndex * (100 / Global.checkedList.Count), $"Copying folder {Path.Combine(appDataFolder, item.Name)}");
                        bool success;

                        if(verboseLogs)
                            success = Utils.CopyDirectoryWithLogging(worker, sourceDir, Path.Combine(Global.TempPath, appDataFolder, Path.GetFileName(item.Name)!), true, ignoreInaccesibleFiles, true);
                        else
                            success = Utils.CopyDirectory(sourceDir, Path.Combine(Global.TempPath, appDataFolder, Path.GetFileName(item.Name)!), true, ignoreInaccesibleFiles, true);
                        
                        if(!success)
                        {
                            worker.CancelAsync();
                            worker.ReportProgress(-1, false);
                            throw new IOException("Something went wrong when copying folders, backup stopped.");
                        }

                        folderIndex++;
                        worker.ReportProgress(folderIndex * (100 / Global.checkedList.Count), $"Folder {Path.Combine(appDataFolder, item.Name)} succesfully copied");
                    }

                    if(worker.CancellationPending)
                    {
                        worker.ReportProgress(-1, "Cancelling...");
                        worker.ReportProgress(-1, false);
                        break;
                    }
                }

                worker.ReportProgress(99, "Creating archive (this may take a while, especially if there are a lot of files)");
                worker.ReportProgress(-1, false);

                ZipFile.CreateFromDirectory(Global.TempPath, fileName);
            }
            catch (Exception ex)
            {
                worker.ReportProgress(-1, "Exception! Backup stopped");
                Utils.ShowErrorMsg(ErrorCode.BackupException, ex.Message);
            }

            worker.ReportProgress(100, "Deleting temp folder");
            Utils.SafeDeleteTempFolder();
        }

        private void DoRestore(String archivePath, BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ZipArchive archive = ZipFile.OpenRead(archivePath))
            {
                // create a temporary folder and extract the backup into it so we can eventually rule out some of the folders we don't want to restore
                // todo: fast restore option that skips this step
                Directory.CreateDirectory(Global.TempPath);
                try
                {
                    int totalEntries = archive.Entries.Count;
                    int extractedEntries = 0;

                    String prevMainFolderName = "";

                    double progress = 0.0;

                    // sort the entries for more accurate logs - may be slow?
                    List<ZipArchiveEntry> sortedEntries = archive.Entries.OrderBy(e => e.FullName).ToList();

                    foreach (ZipArchiveEntry entry in sortedEntries)
                    {
                        String dirPath = Path.Combine(Global.TempPath, entry.FullName);
                        dirPath = dirPath.Remove(dirPath.LastIndexOf('/'));

                        Directory.CreateDirectory(dirPath);

                        String filePath = Path.Combine(Global.TempPath, entry.FullName);
                        if (filePath.Last() == '/')
                            filePath = filePath.Remove(filePath.Length - 1);

                        String mainFolderName = entry.FullName.Remove(entry.FullName.IndexOf('/', entry.FullName.IndexOf('/') + 1));

                        if (prevMainFolderName == "")
                        {
                            worker.ReportProgress((int)progress, $"Started extracting folder {mainFolderName}");
                            prevMainFolderName = mainFolderName;
                        }

                        if (mainFolderName != prevMainFolderName)
                        {
                            worker.ReportProgress((int)progress, $"Finished extracting folder {prevMainFolderName}");
                            worker.ReportProgress(-1, $"Started extracting folder {mainFolderName}");

                            prevMainFolderName = mainFolderName;
                        }

                        extractedEntries++;
                        progress = 100.0 / totalEntries * extractedEntries;

                        // lazy way to manage empty folders but it works
                        try
                        {
                            entry.ExtractToFile(filePath, true);

                            if(verboseLogs)
                                worker.ReportProgress(-1, $"Extracted file {entry.FullName}");
                        }
                        catch (UnauthorizedAccessException)
                        {
                            if (verboseLogs)
                                worker.ReportProgress(-1, $"Attempted to extract empty folder, skipping: {entry.FullName}");
                        }

                        if(worker.CancellationPending)
                        {
                            worker.ReportProgress(-1, "Cancelling...");
                            worker.ReportProgress(-1, false);
                            Utils.SafeDeleteTempFolder();
                            return;
                        }
                    }

                    // searching for every AppData item in the backup, to check if they already exist in AppData
                    String[] itemNames = archive.Entries
                            .Select(e => e.FullName.Remove(e.FullName.IndexOf('/', e.FullName.IndexOf('/') + 1)))
                            .Distinct(StringComparer.OrdinalIgnoreCase)
                            .ToArray();

                    foreach (string item in itemNames)
                    {
                        if (Directory.Exists(Path.Combine(Global.AppDataPath, item)) && !autoOverwrite)
                        {
                            // todo: custom MsgBox class that can manage custom button layouts like Yes/No/Yes to All
                            String caption = "Restore";
                            String message = $"The folder {item} already exists on this computer. Overwrite existing files with the backup? (This will irreversibly replace them!)";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            MessageBoxIcon icon = MessageBoxIcon.Information;

                            DialogResult res = MessageBox.Show(message, caption, buttons, icon);
                            if (res == System.Windows.Forms.DialogResult.No)
                            {
                                worker.ReportProgress(100, $"Removing duplicate folder {item}");
                                Directory.Delete(Path.Combine(Global.TempPath, item), true);
                            }
                        }

                        if (worker.CancellationPending)
                        {
                            worker.ReportProgress(-1, "Cancelling...");
                            worker.ReportProgress(-1, false);
                            Utils.SafeDeleteTempFolder();
                            return;
                        }
                    }

                    worker.ReportProgress(100, "Copying folders to AppData");
                    bool success;

                    if (verboseLogs)
                        success = Utils.CopyDirectoryWithLogging(worker, Global.TempPath, Global.AppDataPath, true, ignoreInaccesibleFiles);
                    else
                        success = Utils.CopyDirectory(Global.TempPath, Global.AppDataPath, true, ignoreInaccesibleFiles);

                    if (!success)
                    {
                        worker.CancelAsync();
                        worker.ReportProgress(-1, false);
                        throw new IOException("Something went wrong when copying folders, backup stopped.");
                    }

                    worker.ReportProgress(100, "Deleting temp folder");
                    Utils.SafeDeleteTempFolder();
                }
                catch (Exception ex)
                {
                    worker.ReportProgress(0, "Exception! Restore stopped");
                    Utils.ShowErrorMsg(ErrorCode.RestoreException, ex.Message);
                }
            }
        }
    }
}
