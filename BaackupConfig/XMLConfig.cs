using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace BaackupConfig
{
    public class XMLConfig
    {
        public static void GenerateDefaultConfig()
        {
            // RCON
            Variables.UseRCON = false;
            Variables.RCONHostname = "127.0.0.1";
            Variables.RCONPass = null;
            Variables.RCONPort = 25575;

            // Server Platform
            Variables.Platform = "vanilla";

            // Worlds container
            Variables.WorldsContainerActive = false;
            Variables.WorldsContainerPath = null;

            // Backup Messages
            Variables.BackupMessageActive = false;
            Variables.BackupMessage = "say The server is backing up! Prepare for some slight lag!";

            Variables.BackupFinishedMessageActive = false;
            Variables.BackupFinishedMessage = "say Server Backup Completed!";

            // Backup items
            Variables.BackupPlugins = false;
            Variables.BackupLogs = true;

            // Folders
            Variables.BackupContainer = null;
            Variables.UseCustomTMPDir = false;
            Variables.CustomTMPDir = null;

            // Backup Saving
            Variables.BackupPrefix = null;
            Variables.CompressBackups = true;
        }

        public static bool ConfigExists()
        {
            if (File.Exists(Variables.ConfigFilePath))
                return true;
            else
                return false;
        }

        #region Config Save / Load

        public static void LoadConfig()
        {
            if (!ConfigExists())
            {
                GenerateDefaultConfig();
                return;
            }

            try // If anything goes wrong, this should keep it from breaking the whole program
            {
                using (XmlReader reader = XmlReader.Create(Variables.ConfigFilePath))
                {
                    reader.ReadToFollowing("Config"); // Here we specify the config.. not that there's anything else right now
                    reader.MoveToFirstAttribute();
                    Variables.UseRCON = Boolean.Parse(reader.Value.ToLower()); // Use rcon?
                    reader.MoveToNextAttribute();
                    Variables.RCONPass = reader.Value; // Rcon password
                    reader.MoveToNextAttribute();
                    Variables.RCONHostname = reader.Value; // Rcon hostname
                    reader.MoveToNextAttribute();
                    Variables.RCONPort = int.Parse(reader.Value); // Rcon port
                    reader.MoveToNextAttribute();
                    Variables.WorldsContainerActive = Boolean.Parse(reader.Value.ToLower()); // Use worlds container?
                    reader.MoveToNextAttribute();
                    Variables.WorldsContainerPath = reader.Value; // Path to worlds container
                    reader.MoveToNextAttribute();
                    Variables.BackupMessageActive = Boolean.Parse(reader.Value.ToLower()); // Use backup server broadcast?
                    reader.MoveToNextAttribute();
                    Variables.BackupMessage = reader.Value; // Backup server broadcast message?
                    reader.MoveToNextAttribute();
                    Variables.BackupPlugins = Boolean.Parse(reader.Value.ToLower());  // Backup plugins? (Spigot and Bukkit only)
                    reader.MoveToNextAttribute();
                    Variables.BackupLogs = Boolean.Parse(reader.Value.ToLower()); // Backup server logs?
                    reader.MoveToNextAttribute();
                    Variables.BackupContainer = reader.Value; // Where to save the backups?
                    reader.MoveToNextAttribute();
                    Variables.UseCustomTMPDir = Boolean.Parse(reader.Value.ToLower()); // Do we use a custom tmp dir? (Default is {Backups save path}\tmp
                    reader.MoveToNextAttribute();
                    Variables.CustomTMPDir = reader.Value; // If the above is enabled, where is this dir you want?
                    reader.MoveToNextAttribute();
                    Variables.BackupPrefix = reader.Value; // Do you want to prefix your backups?
                    reader.MoveToNextAttribute();
                    Variables.CompressBackups = Boolean.Parse(reader.Value.ToLower()); // Do you want to compress your backups?
                    reader.MoveToNextAttribute();
                    Variables.Platform = reader.Value; // Platform? (Spigot/CraftBukkit/Vanilla)
                    reader.MoveToNextAttribute();
                    Variables.BackupFinishedMessageActive = Boolean.Parse(reader.Value.ToLower()); // Use the backup finished message?
                    reader.MoveToNextAttribute();
                    Variables.BackupFinishedMessage = reader.Value; // What is the backup finished message?
                }
            }
            catch (Exception e)
            {
                // If something goes wrong, then actually do nothing
                string ex = e.Message;
            }
        }

        public static void SaveConfig()
        {
            // Create a new XMLWriter
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            // Make sure we don't break everything if some error is thrown

            try
            {
                XmlWriter writer = XmlWriter.Create(Variables.ConfigFilePath, settings);

                writer.WriteStartDocument();
                writer.WriteComment("This is the config file for Baackup, NEVER share or upload this file anywhere on the internet as it could contain your server's RCON password and info in it. Please use the included Configuration tool to edit this file.");
                writer.WriteStartElement("Config");
                writer.WriteAttributeString("usercon", Variables.UseRCON.ToString());
                writer.WriteAttributeString("rconpass", Variables.RCONPass);
                writer.WriteAttributeString("rconhostname", Variables.RCONHostname);
                writer.WriteAttributeString("rconport", Variables.RCONPort.ToString());
                writer.WriteAttributeString("worldscontaineractive", Variables.WorldsContainerActive.ToString());
                writer.WriteAttributeString("worldscontainerpath", Variables.WorldsContainerPath);
                writer.WriteAttributeString("backupmsgactive", Variables.BackupMessageActive.ToString());
                writer.WriteAttributeString("backupmsg", Variables.BackupMessage);
                writer.WriteAttributeString("backupplugins", Variables.BackupPlugins.ToString());
                writer.WriteAttributeString("backuplogs", Variables.BackupLogs.ToString());
                writer.WriteAttributeString("backupcontainer", Variables.BackupContainer);
                writer.WriteAttributeString("usecustomtmpdir", Variables.UseCustomTMPDir.ToString());
                writer.WriteAttributeString("customtmpdir", Variables.CustomTMPDir);
                writer.WriteAttributeString("backupscustomidprefix", Variables.BackupPrefix);
                writer.WriteAttributeString("compressbackups", Variables.CompressBackups.ToString());
                writer.WriteAttributeString("platform", Variables.Platform);
                writer.WriteAttributeString("backupfinishmessageactive", Variables.BackupFinishedMessageActive.ToString());
                writer.WriteAttributeString("backupfinishedmessage", Variables.BackupFinishedMessage);

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                /* do something with this */
                string ex = e.Message;
            }
        }

        #endregion Config Save / Load
    }
}
