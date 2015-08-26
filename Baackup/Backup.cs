using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Baackup
{
    public class Backup
    {
        /// <summary>
        /// Starts the backup with given config
        /// </summary>
        public static void StartBackup()
        {
            // Backup start
            RCON.Send("save-off");
            RCON.Send("save-all");
            Tools.Log("Wait 3 secs...");
            Tools.Wait(3);

            // Tell players backup is starting
            if (Program.BackupMessageActive)
                RCON.Send(Program.BackupMessage);

            #region Copy Server Files

            string[] serverfiles = { "server.properties", "ops.json", "whitelist.json", "banned-ips.json", "banned-players.json" }; // -- A wild array has appeared! --

            foreach (string file in serverfiles) // Copy each file from the array
                CopyFile(file);

            if (Program.Platform == "spigot")
            {
                string[] spigotfiles = { "spigot.yml", "bukkit.yml", "commands.yml", "help.yml", "permissions.yml" }; // Yay it's an array

                foreach (string file in spigotfiles) // Copy each file from the array
                    CopyFile(file);
            }

            if (Program.Platform == "craftbukkit")
            {
                string[] bukkitfiles = { "permissions.yml", "bukkit.yml", "commands.yml", "help.yml" }; // Yay it's another array

                foreach (string file in bukkitfiles) // Copy each file from the array
                    CopyFile(file);
            }

            #endregion

            #region Worlds Container

            if (!Program.WorldsContainerActive)
            {
                string[] worlds = Directory.GetDirectories(Program.ExecutablePath);

                foreach (string world in worlds)
                {
                    if (File.Exists(world + "\\level.dat"))
                        CopyFolder(world);
                }
            }
            else
            {
                CopyFolder(Program.WorldsContainerPath);
            }

            #endregion

            #region Backup Plugins

            if ((Program.Platform == "spigot" || Program.Platform == "craftbukkit") && Program.BackupPlugins)
                CopyFolder("plugins");

            #endregion

            #region Backup Logs

            if (Program.BackupLogs)
                CopyFolder("logs");

            #endregion

            // Copyfiles end
            RCON.Send("save-on");
            Tools.Log("Done copying files!");

            #region Compress / Move

            // Compress to final directory
            if (Program.CompressBackups)
            {
                Compress();
                Tools.Log("Compression complete!");
            }

            if (!Program.CompressBackups)
            {
                Tools.Log("No compression enabled, copying files");

                // Set root for file save
                string filesave = Program.BackupSaveContainer + "\\" + Program.BackupID;

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(Program.TemporarySaveLocation, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(Program.TemporarySaveLocation, filesave + "\\"));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(Program.TemporarySaveLocation, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(Program.TemporarySaveLocation, filesave + "\\"), true);
            }

            Directory.Delete(Program.TemporarySaveLocation, true);

            #endregion

            // Tell players backup is complete
            if (Program.BackupFinishedMessageActive)
                RCON.Send(Program.BackupFinishedMessage);

            // Wait one second then terminate program
            Tools.Wait(1);
            Tools.Exit(0);
        }

        #region Other Stuff

        /// <summary>
        /// Copies the selected file into the backup save directory.
        /// </summary>
        /// <param name="file">File Name</param>
        static void CopyFile(string file)
        {
            try
            {
                File.Copy(Program.ExecutablePath + file, Program.TemporarySaveLocation + file);
                Tools.Log("Copied File:" + file);
            }
            catch (Exception e)
            {
                Tools.Log("Could not copy file \"" + file + "\": " + e.Message);
            }
        }

        /// <summary>
        /// Copies the selected folder into the backup save directory.
        /// </summary>
        /// <param name="folder">Folder Name</param>
        static void CopyFolder(string folder)
        {
            folder = Program.ExecutablePath + new DirectoryInfo(folder).Name;

            try
            {
                Tools.Log("Copying Directory: " + folder);

                //Create start directory
                Directory.CreateDirectory(Program.TemporarySaveLocation + new DirectoryInfo(folder).Name);

                //Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(folder, "*",
                    SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(folder, Program.TemporarySaveLocation + new DirectoryInfo(folder).Name));
                    Tools.Log(folder + ": Create subdirectory with name " + dirPath.Replace(folder, Program.TemporarySaveLocation + folder));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(folder, "*.*",
                    SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(folder, Program.TemporarySaveLocation + new DirectoryInfo(folder).Name), true);
                    Tools.Log(folder + ": Create file with name " + Path.GetFileName(newPath.Replace(folder, Program.TemporarySaveLocation + folder)) + " inside " + Path.GetDirectoryName(newPath.Replace(folder, Program.TemporarySaveLocation + folder)));
                }
            }
            catch (Exception e)
            {
                Tools.Log("Could not copy the directory \"" + folder + "\": " + e.Message);
            }
        }

        /// <summary>
        /// Compresses the temporary files into the backup save container.
        /// </summary>
        static void Compress()
        {
            ProcessStartInfo p = new ProcessStartInfo(); // Set processinfo

            p.FileName = Program.ExecutablePath + "7z.exe"; // Set filename

            // EXAMPLE FOR BELOW: a -t7z "C:\Backups\backup-{ID}.7z" "C:\backups\tmp\backup-{ID}\"
            p.Arguments = "a -t7z \"" + Program.BackupSaveContainer + "\\" + Program.BackupID + ".7z\" \"" + Program.TemporarySaveLocation + "\\\""; // Set args
            p.WindowStyle = ProcessWindowStyle.Hidden; // Hde window

            Tools.Log("Compressing (This may take a long time)");

            Process x = Process.Start(p); // Start Process and wait to exit.
            x.WaitForExit();
        }

        #endregion
    }
}
