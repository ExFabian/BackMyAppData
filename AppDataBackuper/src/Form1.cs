using System.Collections.Immutable;
using System.ComponentModel;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AppDataBackuper
{
    public partial class Form1 : Form
    {
        bool noRegisterChecks = false; // used so the function ItemList_ItemCheck doesn't run whenever the current AppData changes

        public Form1()
        {
            InitializeComponent();

            this.Text = Global.appName + " " + Global.appVersion;

            bool success = Utils.SafeDeleteTempFolder();
            if (!success)
                Environment.Exit(1);

            ItemList_Load();
        }

        private void ItemList_Load()
        {
            noRegisterChecks = true;

            ItemList.Items.Clear();
            ItemContentListBox.Items.Clear();

            Global.selectedItem = null;
            OpenFolderButton.Enabled = false;

            String[] itemNames = { };

            try
            {
                itemNames = Directory.GetDirectories(Utils.GetAppDataPath())                        // Get all directories in the selected AppData
                    .Where(e => !new DirectoryInfo(e).Attributes.HasFlag(FileAttributes.Hidden))    // excluding the hidden directories
                    .Select(e => Path.Combine(Utils.GetAppDataPath(), Path.GetFileName(e)))         // Flatten the directory paths
                    .ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMsg(ErrorCode.FetchDirException, ex.Message);
                return;
            }

            foreach (String name in itemNames)
            {
                CheckState state = CheckState.Unchecked;
                if (Global.checkedList.Any(e => e.Name == name))
                    state = CheckState.Checked;

                ItemList.Items.Add(Path.GetFileName(name), state);
            }

            noRegisterChecks = false;
        }

        private void ItemContentListBox_Load(object item)
        {
            noRegisterChecks = true;

            String itemName = Path.Combine(Utils.GetAppDataPath(), item.ToString()!);

            String[] itemContents = { };

            try
            {
                itemContents = Directory.GetFileSystemEntries(itemName);
            }
            catch (DirectoryNotFoundException)
            {
                Utils.ShowErrorMsg(ErrorCode.MiscDirectoryNotFound, itemName);
                return;
            }
            catch (Exception ex)
            {
                Utils.ShowErrorMsg(ErrorCode.MiscException, ex.Message);
                return;
            }

            if (itemContents.Length == 0) return;

            ItemContentListBox.Items.Clear();
            foreach (String name in itemContents)
            {
                String actualName = Path.GetFileName(name);
                CheckState state = CheckState.Checked;
                if (Global.checkedList.Where(e => e.Name == itemName)
                    .Any(e => e.SubItem
                    .Any(sub => sub.Name == actualName && sub.State == CheckState.Unchecked))
                )
                    state = CheckState.Unchecked;

                ItemContentListBox.Items.Add(actualName, state);
            }

            noRegisterChecks = false;
        }

        private void ItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.selectedItem = ItemList.SelectedItem;
            if (Global.selectedItem == null)
            {
                OpenFolderButton.Enabled = false;
                return;
            }

            OpenFolderButton.Enabled = true;

            ItemContentListBox_Load(Global.selectedItem);
        }

        private void ItemList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (noRegisterChecks) return;

            if (e.NewValue == CheckState.Checked)
            {
                String name = Path.Combine(Utils.GetAppDataPath(), ItemList.Items[e.Index].ToString()!);
                Global.checkedList.Add(new CheckedItem(name));

                for (int i = 0; i < ItemContentListBox.Items.Count; i++)
                {
                    Global.checkedList.Last().SubItem.Add((ItemContentListBox.Items[i].ToString()!, ItemContentListBox.GetItemCheckState(i)));
                }

                // because Count changes after ItemCheck runs for some reason
                switch (Global.selectedAppData)
                {
                    case AppData.Local:
                        LocalCountLabel.Text = "Count: " + (ItemList.CheckedItems.Count + 1);
                        break;

                    case AppData.Roaming:
                        RoamingCountLabel.Text = "Count: " + (ItemList.CheckedItems.Count + 1);
                        break;

                    case AppData.LocalLow:
                        LocalLowCountLabel.Text = "Count: " + (ItemList.CheckedItems.Count + 1);
                        break;
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                String name = Path.Combine(Utils.GetAppDataPath(), ItemList.Items[e.Index].ToString()!);
                Global.checkedList.Remove(Global.checkedList
                    .Where(e => e.Name == name)
                    .First());

                switch (Global.selectedAppData)
                {
                    case AppData.Local:
                        LocalCountLabel.Text = "Count: " + (ItemList.CheckedItems.Count - 1);
                        break;

                    case AppData.Roaming:
                        RoamingCountLabel.Text = "Count: " + (ItemList.CheckedItems.Count - 1);
                        break;

                    case AppData.LocalLow:
                        LocalLowCountLabel.Text = "Count: " + (ItemList.CheckedItems.Count - 1);
                        break;
                }
            }
        }

        private void ItemContentListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (noRegisterChecks) return;
            if (!ItemList.CheckedItems.Contains(ItemList.SelectedItem)) return;

            int itemIndex = Global.checkedList.IndexOf(Global.checkedList
                .Where(e => e.Name == Path.Combine(Utils.GetAppDataPath(), ItemList.Items[ItemList.SelectedIndex].ToString()!))
                .First());

            Global.checkedList[itemIndex] = new CheckedItem(Global.checkedList[itemIndex].Name);
            for (int i = 0; i < ItemContentListBox.Items.Count; i++)
            {
                CheckState state;
                if (i == e.Index)
                    state = e.NewValue;
                else
                    state = ItemContentListBox.GetItemCheckState(i);

                Global.checkedList[itemIndex].SubItem.Add((ItemContentListBox.Items[i].ToString()!, state));
            }
        }

        private void LocalButton_Click(object sender, EventArgs e)
        {
            if (Global.selectedAppData == AppData.Local) return;

            Global.selectedAppData = AppData.Local;
            ItemList_Load();
        }

        private void RoamingButton_Click(object sender, EventArgs e)
        {
            if (Global.selectedAppData == AppData.Roaming) return;

            Global.selectedAppData = AppData.Roaming;
            ItemList_Load();
        }

        private void LocalLowButton_Click(object sender, EventArgs e)
        {
            if (Global.selectedAppData == AppData.LocalLow) return;

            Global.selectedAppData = AppData.LocalLow;
            ItemList_Load();
        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            if (Global.selectedItem == null) return;
            System.Diagnostics.Process.Start("explorer", Path.Combine(Utils.GetAppDataPath(), Global.selectedItem.ToString()!));
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ItemList.Items.Count; i++)
            {
                if (!ItemList.GetItemChecked(i))
                    ItemList.SetItemChecked(i, true);
            }
        }

        private void DeselectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ItemList.Items.Count; i++)
            {
                if (ItemList.GetItemChecked(i))
                    ItemList.SetItemChecked(i, false);
            }
        }

        private void SelectAllSubButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ItemContentListBox.Items.Count; i++)
            {
                if (!ItemContentListBox.GetItemChecked(i))
                    ItemContentListBox.SetItemChecked(i, true);
            }
        }

        private void DeselectAllSubButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ItemContentListBox.Items.Count; i++)
            {
                if (ItemContentListBox.GetItemChecked(i))
                    ItemContentListBox.SetItemChecked(i, false);
            }
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            AboutBox1 aform = new AboutBox1();
            aform.ShowDialog();
            aform.Dispose();
        }

        private void SoloBackupButton_Click(object sender, EventArgs e)
        {
            if (Global.checkedList.Count == 0)
            {
                Utils.ShowErrorMsg(ErrorCode.BackupNoCheckedItems);
                return;
            }

            SaveFileDialog saveFileDialogBackup = new SaveFileDialog();
            saveFileDialogBackup.Filter = "Zip File|*.zip";
            saveFileDialogBackup.Title = "Backup Items";

            if (Global.checkedList.Count == 1)
                saveFileDialogBackup.FileName = Path.GetFileName(Global.checkedList[0].Name) + "_backup_" + DateTime.Now.ToString("yyMMddmmss") + ".zip";
            else
                saveFileDialogBackup.FileName = "AppData_backup_" + DateTime.Now.ToString("yyMMddmmss") + ".zip";

            DialogResult res = saveFileDialogBackup.ShowDialog();

            if (res != DialogResult.OK) { return; }

            if (saveFileDialogBackup.FileName == "")
            {
                Utils.ShowErrorMsg(ErrorCode.BackupEmptyFileName);
                return;
            }

            ProgressForm pform = new ProgressForm();

            pform.Reset(RunMode.Backup, saveFileDialogBackup.FileName);

            if (Global.checkedList.Count == 1)
                pform.Log($"Ready to backup the following item:");
            else
                pform.Log($"Ready to backup the following {Global.checkedList.Count} items:");

            foreach (CheckedItem item in Global.checkedList)
            {
                pform.LogNoScroll(item.Name);
            }

            pform.ShowDialog(this);
            pform.Dispose();
        }

        private void SoloRestoreButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogRestore = new OpenFileDialog();
            openFileDialogRestore.Filter = "Zip File|*.zip";
            openFileDialogRestore.Title = "Restore Item";
            openFileDialogRestore.ShowDialog();

            if (openFileDialogRestore.FileName == "")
            {
                Utils.ShowErrorMsg(ErrorCode.RestoreEmptyFileName);
                return;
            }
            if (Path.GetExtension(openFileDialogRestore.FileName) != ".zip")
            {
                Utils.ShowErrorMsg(ErrorCode.RestoreInvalidFile);
                return;
            }

            using (ZipArchive archive = ZipFile.OpenRead(openFileDialogRestore.FileName))
            {
                // check if any of the AppData root folders are present
                string[] foldersToCheck = { "Local/", "Roaming/", "LocalLow/" };

                bool valid = archive.Entries.Any(e => foldersToCheck
                    .Any(folder => e.FullName
                    .StartsWith(folder, StringComparison.OrdinalIgnoreCase)));

                if (!valid)
                {
                    Utils.ShowErrorMsg(ErrorCode.RestoreInvalidFile);
                    return;
                }

                ProgressForm pform = new ProgressForm();

                pform.Reset(RunMode.Restore, openFileDialogRestore.FileName);

                pform.Log($"Ready to restore backup file {openFileDialogRestore.FileName}");
                pform.Log("If you got the backup file from an untrusted source, check its contents with an archiver program before restoring it!");
                pform.Log("Folders to be restored:");

                try
                {
                    // get only the 2nd level folder entries: "Local/item1", "Roaming/item2", etc.
                    String[] entryNames = archive.Entries
                            .Select(e => e.FullName.Remove(e.FullName.IndexOf('/', e.FullName.IndexOf('/') + 1)))
                            .Distinct(StringComparer.OrdinalIgnoreCase)
                            .ToArray();

                    foreach (String entry in entryNames)
                    {
                        pform.LogNoScroll(entry);
                    }
                }
                catch (Exception)
                {
                    Utils.ShowErrorMsg(ErrorCode.RestoreFileUnknownFormat);
                    return;
                }

                pform.ShowDialog(this);
                pform.Dispose();
            }
        }
    }
}
