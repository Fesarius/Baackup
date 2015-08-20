﻿using System;
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

                // Bukkit
                if (Platform_CraftBukkit.Checked)
                {
                    WorldsContainerButton.Enabled = false;
                    WorldsContainerPathTextBox.Enabled = false;
                    WorldsContainerPathBrowseButton.Enabled = false;
                }

                // Spigot
                if (Platform_Spigot.Checked)
                {
                    WorldsContainerButton.Enabled = true;
                    WorldsContainerPathTextBox.Enabled = true;
                    WorldsContainerPathBrowseButton.Enabled = true;
                }
            }
            else // Vanilla
            {
                // Disable modded server options
                ModdedOptions.Enabled = false;
            }

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

            #endregion

            #endregion

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

            #endregion

            #region Backup Messages

            if (BackupStartedMessageEnabledBox.Checked)
                BackupStartedMessageTextBox.Enabled = true;
            else
                BackupStartedMessageTextBox.Enabled = false;


            if (BackupFinishedMessageEnabledBox.Checked)
                BackupFinishedMessageTextBox.Enabled = true;
            else
                BackupFinishedMessageTextBox.Enabled = false;

            #endregion

            #region TMP Save Location

            if (TmpSaveLocationEnabledBox.Checked)
                TmpSaveLocationTextBox.Enabled = true;
            else
                TmpSaveLocationTextBox.Enabled = false;

            #endregion
        }

        #region Other Stuff That Makes The Program Do Stuff

        #region Functional Things

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

        #endregion

        #region Actions

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
            GUIUpdate();
        }

        private void ReloadConfig_Click(object sender, EventArgs e)
        {
            XMLConfig.LoadConfig();
            GUIUpdate();
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            XMLConfig.SaveConfig();

            // Update just to be safe
            GUIUpdate();
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

        #endregion

        #endregion
    }
}
