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
                using (XmlReader reader = XmlReader.Create(Program.configfile))
                {
                    reader.ReadToFollowing("Config"); // Here we specify the config.. not that there's anything else right now
                    reader.MoveToFirstAttribute();
                    Program.usercon = Boolean.Parse(reader.Value.ToLower()); // Use rcon?
                    reader.MoveToNextAttribute();
                    Program.rconpass = reader.Value; // Rcon password
                    reader.MoveToNextAttribute();
                    Program.rconhostname = reader.Value; // Rcon hostname
                    reader.MoveToNextAttribute();
                    Program.rconport = int.Parse(reader.Value); // Rcon port
                    reader.MoveToNextAttribute();
                    Program.worldscontaineractive = Boolean.Parse(reader.Value.ToLower()); // Use worlds container?
                    reader.MoveToNextAttribute();
                    Program.worldscontainerpath = reader.Value; // Path to worlds container
                    reader.MoveToNextAttribute();
                    Program.backupmsgactive = Boolean.Parse(reader.Value.ToLower()); // Use backup server broadcast?
                    reader.MoveToNextAttribute();
                    Program.backupmsg = reader.Value; // Backup server broadcast message?
                    reader.MoveToNextAttribute();
                    Program.backupplugins = Boolean.Parse(reader.Value.ToLower());  // Backup plugins? (Spigot and Bukkit only)
                    reader.MoveToNextAttribute();
                    Program.backuplogs = Boolean.Parse(reader.Value.ToLower()); // Backup server logs?
                    reader.MoveToNextAttribute();
                    Program.backupcontainer = reader.Value; // Where to save the backups?
                    reader.MoveToNextAttribute();
                    Program.usecustomtmpdir = Boolean.Parse(reader.Value.ToLower()); // Do we use a custom tmp dir? (Default is {Backups save path}\tmp
                    reader.MoveToNextAttribute();
                    Program.customtmpdir = reader.Value; // If the above is enabled, where is this dir you want?
                    reader.MoveToNextAttribute();
                    Program.backupscustomidprefix = reader.Value; // Do you want to prefix your backups?
                    reader.MoveToNextAttribute();
                    Program.compressbackups = Boolean.Parse(reader.Value.ToLower()); // Do you want to compress your backups?
                    reader.MoveToNextAttribute();
                    Program.platform = reader.Value; // Platform? (Spigot/CraftBukkit/Vanilla)
                    reader.MoveToNextAttribute();
                    Program.backupfinishmsgactive = Boolean.Parse(reader.Value.ToLower()); // Display a message when we're finished backing up?
                    reader.MoveToNextAttribute();
                    Program.backupfinishmsg = reader.Value; // What is the backup finished message?
                }
            }
            catch (Exception e)
            {
                // If something goes wrong, maybe this will help.
                Tools.Print("Error:\n" + e.Message + Environment.NewLine + Program.configfile);
                Tools.Pause("Press any key to terminate");
                if (e.Message.ToLower().StartsWith("could not find file"))
                    Tools.Exit(2);
                else
                    Tools.Exit(1);
            }
        }

        #endregion Config Loading

        #region Other Stuff

        public static bool ConfigExists()
        {
            if (File.Exists(Program.configfile))
                return true;
            else
                return false;
        }

        #endregion Other Stuff
    }
}
