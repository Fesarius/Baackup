using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Baackup
{
    public class XMLConfig
    {
        #region Config Loading

        public static void LoadConfig()
        {
            try // If anything goes wrong, this should keep it from breaking the whole program
            {
                using (XmlReader reader = XmlReader.Create(Program.ConfigurationFilePath))
                {
                    reader.ReadToFollowing("Config"); // Here we specify the config.. not that there's anything else right now
                    reader.MoveToFirstAttribute();
                    Program.UseRCON = Boolean.Parse(reader.Value.ToLower()); // Use rcon?
                    reader.MoveToNextAttribute();
                    Program.RCONPassword = reader.Value; // Rcon password
                    reader.MoveToNextAttribute();
                    Program.RCONHostname = reader.Value; // Rcon hostname
                    reader.MoveToNextAttribute();
                    Program.RCONPort = int.Parse(reader.Value); // Rcon port
                    reader.MoveToNextAttribute();
                    Program.WorldsContainerActive = Boolean.Parse(reader.Value.ToLower()); // Use worlds container?
                    reader.MoveToNextAttribute();
                    Program.WorldsContainerPath = reader.Value; // Path to worlds container
                    reader.MoveToNextAttribute();
                    Program.BackupMessageActive = Boolean.Parse(reader.Value.ToLower()); // Use backup server broadcast?
                    reader.MoveToNextAttribute();
                    Program.BackupMessage = reader.Value; // Backup server broadcast message?
                    reader.MoveToNextAttribute();
                    Program.BackupPlugins = Boolean.Parse(reader.Value.ToLower());  // Backup plugins? (Spigot and Bukkit only)
                    reader.MoveToNextAttribute();
                    Program.BackupLogs = Boolean.Parse(reader.Value.ToLower()); // Backup server logs?
                    reader.MoveToNextAttribute();
                    Program.BackupSaveContainer = reader.Value; // Where to save the backups?
                    reader.MoveToNextAttribute();
                    Program.UseCustomTempDirectory = Boolean.Parse(reader.Value.ToLower()); // Do we use a custom tmp dir? (Default is {Backups save path}\tmp
                    reader.MoveToNextAttribute();
                    Program.CustomTempDirectory = reader.Value; // If the above is enabled, where is this dir you want?
                    reader.MoveToNextAttribute();
                    Program.BackupSavePrefix = reader.Value; // Do you want to prefix your backups?
                    reader.MoveToNextAttribute();
                    Program.CompressBackups = Boolean.Parse(reader.Value.ToLower()); // Do you want to compress your backups?
                    reader.MoveToNextAttribute();
                    Program.Platform = reader.Value; // Platform? (Spigot/CraftBukkit/Vanilla)
                    reader.MoveToNextAttribute();
                    Program.BackupFinishedMessageActive = Boolean.Parse(reader.Value.ToLower()); // Display a message when we're finished backing up?
                    reader.MoveToNextAttribute();
                    Program.BackupFinishedMessage = reader.Value; // What is the backup finished message?
                }
            }
            catch (Exception e)
            {
                // If something goes wrong, maybe this will help.
                Tools.Print("Error:\n" + e.Message + Environment.NewLine + Program.ConfigurationFilePath);
                Tools.Pause("Press any key to terminate");
                if (e.Message.ToLower().StartsWith("could not find file"))
                    Tools.Exit(2);
                else
                    Tools.Exit(1);
            }
        }

        #endregion

        #region Other Stuff

        public static bool ConfigExists()
        {
            if (File.Exists(Program.ConfigurationFilePath))
                return true;
            else
                return false;
        }

        #endregion
    }
}
