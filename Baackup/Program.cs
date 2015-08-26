using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Baackup
{
    class Program
    {
        #region Class Variables

        #region RCON

        public static bool UseRCON;
        public static string RCONPassword;
        public static string RCONHostname;
        public static int RCONPort;

        #endregion

        #region Worlds Container

        public static bool WorldsContainerActive;
        public static string WorldsContainerPath;

        #endregion

        #region Backup Messages

        public static bool BackupMessageActive;
        public static string BackupMessage;

        public static bool BackupFinishedMessageActive;
        public static string BackupFinishedMessage;

        #endregion

        #region Backup Toggles and Server Info

        public static bool BackupPlugins;
        public static bool BackupLogs;

        public static string Platform;

        #endregion

        #region Directory and Saving Info

        public static string BackupSaveContainer;
        public static string BackupSavePrefix;

        public static bool UseCustomTempDirectory;
        public static string CustomTempDirectory;

        public static bool CompressBackups;

        #endregion

        #region Other Stuff

        // Config path
        public static string ConfigurationFilePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\baackupconfig.xml");

        // Execution path
        public static string ExecutablePath;

        // TMP Backup path
        public static string TemporarySaveLocation;
        public static string BackupID;

        #endregion

        #endregion

        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args">Arugments from batch file</param>
        static void Main(string[] args)
        {
            Tools.Title("Baackup for Minecraft Server - Technoguyfication " + DateTime.Now.Year.ToString());
            try
            {
                ExecutablePath = args[0] + '\\';
            }
            catch (Exception e)
            {
                Tools.Print("There was an issue starting the program!\nAre you sure you started it with the Batch script?\n\nError details: " + e.Message);
                Tools.Pause();
                Tools.Exit(1);
            }

            new Program().Start();
            Tools.Exit(1);
        }

        /// <summary>
        /// Starts the program fully
        /// </summary>
        public void Start()
        {
            // Display title and intro text
            Tools.Print("Baackup for Minecraft Server - Technoguyfication " + DateTime.Now.Year.ToString());
            Tools.Print("Running on path: " + ExecutablePath);

            if (XMLConfig.ConfigExists())
            {
                // Load program configuration from file
                XMLConfig.LoadConfig();
                Tools.NewBackupID();

                // Validate Configuration
                if (!Tools.ValidateConfig())
                {
                    Tools.Log("Cannot continue with invalid configuration. Exiting program...", "Fatal");

                    Tools.Wait(5);
                    Tools.Exit(1);
                }

                // Tmp dir settings
                if (UseCustomTempDirectory)
                    TemporarySaveLocation = CustomTempDirectory + "\\" + BackupID + "\\";
                else
                    TemporarySaveLocation = BackupSaveContainer + "\\tmp\\" + BackupID + "\\";

                // Check that we are running in a server directory.
                if (!IOStatus.FileExists("server.properties", true))
                {
                    Tools.Print("We did not detect you are running in a server directory!\nPlease but the batch file in your root directory\nwith the jar/exe file and server.properties!");
                    Tools.Pause();
                    Tools.Exit(2);
                }
                else
                {
                    Tools.Log("Found server.properties!");

                    // Create tmp dir
                    if (!IOStatus.FolderExists(Program.TemporarySaveLocation, false))
                        Directory.CreateDirectory(Program.TemporarySaveLocation);

                    // Start Backup
                    Backup.StartBackup();
                }
            }
            else
            {
                Tools.Print("Please use the configuration tool to generate a server configuration file.");
                Tools.Pause();
                Tools.Exit();
            }
        }
    }
}
