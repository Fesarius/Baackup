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

        // Config options
        public static bool usercon;
        public static string rconpass;
        public static string rconhostname;
        public static int rconport;
        public static bool worldscontaineractive;
        public static string worldscontainerpath;
        public static bool backupmsgactive;
        public static string backupmsg;
        public static bool backupplugins;
        public static bool backuplogs;
        public static string backupcontainer;
        public static bool usecustomtmpdir;
        public static string customtmpdir;
        public static string backupscustomidprefix;
        public static bool compressbackups;
        public static string platform;
        public static bool backupfinishmsgactive;
        public static string backupfinishmsg;

        // Config path
        public static string configfile = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\baackupconfig.xml");

        // Execution path
        public static string exepath;

        // TMP Backup path
        public static string tmpsave;
        public static string backupid;

        #endregion Class Variables

        static void Main(string[] args)
        {
            Tools.Title("Baackup for Minecraft Server - Technoguyfication " + DateTime.Now.Year.ToString());
            try
            {
                exepath = args[0] + '\\';
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

        public void Start()
        {
            // Display title and intro text
            Tools.Print("Baackup for Minecraft Server - Technoguyfication " + DateTime.Now.Year.ToString());
            Tools.Print("Running on path: " + exepath);

            if (XMLConfig.ConfigExists())
            {
                // Load program configuration from file
                XMLConfig.LoadConfig();
                Tools.NewBackupID();
                if (usecustomtmpdir)
                    tmpsave = customtmpdir + "\\" + backupid + "\\";
                else
                    tmpsave = backupcontainer + "\\tmp\\" + backupid + "\\";

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
                    if (!IOStatus.FolderExists(Program.tmpsave, false))
                        Directory.CreateDirectory(Program.tmpsave);

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
