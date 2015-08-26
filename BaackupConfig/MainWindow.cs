using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BaackupConfig
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void GUIUpdate()
        {
            #region Modded Server Options

            if (Platform_CraftBukkit.Checked || Platform_Spigot.Checked)
            {
                // Enable modded server options
                ModdedOptions.Enabled = true;

                // Worlds container
                WorldsContainerButton.Enabled = true;
                WorldsContainerPathTextBox.Enabled = true;
                WorldsContainerPathBrowseButton.Enabled = true;
            }
            else // Vanilla
            {
                // Disable modded server options
                ModdedOptions.Enabled = false;
            }

            #endregion Modded Server Options

            #region Worlds Container

            if (WorldsContainerButton.Checked)
            {
                WorldsContainerPathTextBox.Enabled = true;
                WorldsContainerPathBrowseButton.Enabled = true;
            }
            else
            {
                WorldsContainerPathTextBox.Enabled = false;
                WorldsContainerPathBrowseButton.Enabled = false;
            }

            #endregion Worlds Container

            #region RCON

            if (UseRCONBox.Checked)
            {
                RCONHostnameTextBox.Enabled = true;
                RCONPortTextBox.Enabled = true;
                RCONPasswordTextBox.Enabled = true;

                MessagesPanel.Enabled = true;
            }
            else
            {
                RCONHostnameTextBox.Enabled = false;
                RCONPortTextBox.Enabled = false;
                RCONPasswordTextBox.Enabled = false;

                MessagesPanel.Enabled = false;
            }

            #endregion RCON

            #region Messages

            if (BackupStartedMessageEnabledBox.Checked) // Backup Started Message Switch
                BackupStartedMessageTextBox.Enabled = true;
            else
                BackupStartedMessageTextBox.Enabled = false;

            if (BackupFinishedMessageEnabledBox.Checked) // Backup Finished Message Switch
                BackupFinishedMessageTextBox.Enabled = true;
            else
                BackupFinishedMessageTextBox.Enabled = false;

            #endregion Messages

            #region Temporary Save Location

            if (TmpSaveLocationEnabledBox.Checked) // Temporary Save Location Switch
                TmpSaveLocationTextBox.Enabled = true;
            else
                TmpSaveLocationTextBox.Enabled = false;

            #endregion Temporary Save Location
        }

        #region Other Stuff

        /// <summary>
        /// Returns the filename-safe version of a string.
        /// </summary>
        /// <param name="Questionable String"></param>
        /// <returns></returns>
        private string FileSafe(string name)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                name = name.Replace(c.ToString(), "");
            }

            name = name.Replace(' ', '-');

            return name;
        }

        /// <summary>
        /// Sends the current saved / loaded config to the GUI.
        /// </summary>
        public void GetConfigSettings()
        {
            // RCON
            UseRCONBox.Checked = Variables.UseRCON;
            RCONHostnameTextBox.Text = Variables.RCONHostname;
            RCONPasswordTextBox.Text = Variables.RCONPass;
            RCONPortTextBox.Text = Variables.RCONPort.ToString();

            // Server Platform
            if (Variables.Platform == "spigot")
            {
                Platform_Spigot.Checked = true;

                Platform_Vanilla.Checked = false;
                Platform_CraftBukkit.Checked = false;
            }
            else if (Variables.Platform == "craftbukkit")
            {
                Platform_CraftBukkit.Checked = true;

                Platform_Spigot.Checked = false;
                Platform_Vanilla.Checked = false;
            }
            else
            {
                Platform_Vanilla.Checked = true;

                Platform_Spigot.Checked = false;
                Platform_CraftBukkit.Checked = false;
            }

            // Worlds container
            WorldsContainerButton.Checked = Variables.WorldsContainerActive;
            WorldsContainerPathTextBox.Text = Variables.WorldsContainerPath;

            // Backup Messages
            BackupStartedMessageEnabledBox.Checked = Variables.BackupMessageActive;
            BackupStartedMessageTextBox.Text = Variables.BackupMessage;

            BackupFinishedMessageEnabledBox.Checked = Variables.BackupFinishedMessageActive;
            BackupFinishedMessageTextBox.Text = Variables.BackupFinishedMessage;

            // Backup items
            BackupPluginsBox.Checked = Variables.BackupPlugins;
            BackupLogsBox.Checked = Variables.BackupLogs;

            // Folders
            BackupContainerTextBox.Text = Variables.BackupContainer;
            TmpSaveLocationEnabledBox.Checked = Variables.UseCustomTMPDir;
            TmpSaveLocationTextBox.Text = Variables.CustomTMPDir;

            // Backup Saving
            BackupPrefixTextBox.Text = Variables.BackupPrefix;
            CompressBackupsBox.Checked = Variables.CompressBackups;
        }

        /// <summary>
        /// Sends the current GUI options to the actual variables.
        /// </summary>
        public void SendConfigSettings()
        {
            // RCON
            Variables.UseRCON = UseRCONBox.Checked;
            Variables.RCONHostname = RCONHostnameTextBox.Text;
            Variables.RCONPass = RCONPasswordTextBox.Text;
            Variables.RCONPort = int.Parse(RCONPortTextBox.Text);

            // Server Platform
            if (Platform_CraftBukkit.Checked)
                Variables.Platform = "craftbukkit";
            else if (Platform_Spigot.Checked)
                Variables.Platform = "spigot";
            else
                Variables.Platform = "vanilla";

            // Worlds container
            Variables.WorldsContainerActive = WorldsContainerButton.Checked;
            Variables.WorldsContainerPath = WorldsContainerPathTextBox.Text;

            // Backup Messages
            Variables.BackupMessageActive = BackupStartedMessageEnabledBox.Checked;
            Variables.BackupMessage = BackupStartedMessageTextBox.Text;

            Variables.BackupFinishedMessageActive = BackupFinishedMessageEnabledBox.Checked;
            Variables.BackupFinishedMessage = BackupFinishedMessageTextBox.Text;

            // Backup items
            Variables.BackupPlugins = BackupPluginsBox.Checked;
            Variables.BackupLogs = BackupLogsBox.Checked;

            // Folders
            Variables.BackupContainer = BackupContainerTextBox.Text;
            Variables.UseCustomTMPDir = TmpSaveLocationEnabledBox.Checked;
            Variables.CustomTMPDir = TmpSaveLocationTextBox.Text;

            // Backup Saving
            Variables.BackupPrefix = BackupPrefixTextBox.Text;
            Variables.CompressBackups = CompressBackupsBox.Checked;
        }

        #region Events

        private void Platform_Spigot_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void Platform_CraftBukkit_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            XMLConfig.LoadConfig();
            GetConfigSettings();

            GUIUpdate();
        }

        private void ReloadConfig_Click(object sender, EventArgs e)
        {
            XMLConfig.LoadConfig();
            GetConfigSettings();

            GUIUpdate();

            // Display message
            MessageBox.Show("Successfully reloaded config settings from file.", "Success");
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            SendConfigSettings();
            XMLConfig.SaveConfig();

            // Update just to be safe
            GUIUpdate();

            // Display message
            MessageBox.Show("Successfully saved config settings to file.", "Success");
        }

        private void WorldsContainerButton_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void WorldsContainerPathBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.Description = "Please select your worlds container folder.";

            if (FolderBrowser.ShowDialog() == DialogResult.OK)
                WorldsContainerPathTextBox.Text = FolderBrowser.SelectedPath;

            GUIUpdate();
        }

        private void Platform_Vanilla_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void RCONPortTextBox_LostFocus(object sender, EventArgs e)
        {
            // Validate
            bool valid = true;

            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            foreach (char character in RCONPortTextBox.Text.ToCharArray())
            {
                int pos = Array.IndexOf(numbers, character);

                if (!(pos > -1))
                    valid = false;
            }

            if (!valid)
            {
                RCONPortTextBox.Text = "";
                MessageBox.Show("Port number cannot contain anything but numbers.", "Input Error");
            }
        }

        private void UseRCONBox_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void BackupPrefixTextBox_LostFocus(object sender, EventArgs e)
        {
            BackupPrefixTextBox.Text = FileSafe(BackupPrefixTextBox.Text);
        }

        private void BackupContainerButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.Description = "Please select where to store your backups.";

            if (FolderBrowser.ShowDialog() == DialogResult.OK)
                BackupContainerTextBox.Text = FolderBrowser.SelectedPath;

            GUIUpdate();
        }

        private void BackupLogsBox_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void TmpSaveLocationEnabledBox_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void BackupStartedMessageEnabledBox_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void BackupFinishedMessageEnabledBox_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        private void TmpSaveLocationBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.Description = "Please select a working / temporary directory to use.";

            if (FolderBrowser.ShowDialog() == DialogResult.OK)
                TmpSaveLocationTextBox.Text = FolderBrowser.SelectedPath;
        }

        private void CompressBackupsBox_CheckedChanged(object sender, EventArgs e)
        {
            GUIUpdate();
        }

        #endregion Events

        #endregion Other Stuff
    }
}
