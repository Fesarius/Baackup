using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Baackup
{
    public class Tools
    {
        /// <summary>
        /// Validates the configuration settings.
        /// </summary>
        /// <returns>Configuration Validity State</returns>
        public static bool ValidateConfig()
        {
            try
            {
                #region RCON

                if (Program.UseRCON)
                {
                    if (string.IsNullOrEmpty(Program.RCONPassword))
                        throw new Exception("RCON Password Blank");
                    else
                        if (string.IsNullOrEmpty(Program.RCONHostname))
                        throw new Exception("RCON Hostname Blank");
                    else
                        if (string.IsNullOrEmpty(Program.RCONPort.ToString()))
                        throw new Exception("RCON Port Blank");
                }

                #endregion

                #region Worlds Container

                if (Program.WorldsContainerActive)
                    if (string.IsNullOrEmpty(Program.WorldsContainerPath))
                        throw new Exception("Worlds Container Path Blank");

                #endregion

                #region Backup Message

                if (Program.BackupMessageActive)
                    if (string.IsNullOrEmpty(Program.BackupMessage))
                        throw new Exception("Backup Started Message is Blank");

                if (Program.BackupFinishedMessageActive)
                    if (string.IsNullOrEmpty(Program.BackupFinishedMessage))
                        throw new Exception("Backup Completed Message is Blank");

                #endregion

                #region Custom Temp Dir

                if (Program.UseCustomTempDirectory)
                {
                    if (string.IsNullOrEmpty(Program.CustomTempDirectory))
                        throw new Exception("Custom Temporary Directory Blank");

                    if (!Directory.Exists(Program.CustomTempDirectory))
                        throw new Exception("Custom Temporary Directory Nonexistent");
                }

                #endregion

                #region Backup Prefix Verification

                string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

                foreach (char c in invalid)
                {
                    if (Program.BackupSavePrefix.Contains(c))
                        throw new Exception("Backup Prefix Contains Invalid Characters");
                }

                #endregion
            }
            catch (Exception e)
            {
                Tools.Log("Could not verify config: " + e.Message, "Error");
                return false;
            }

            // Assuming no errors were thrown
            return true;
        }

        #region Other Stuff

        /// <summary>
        /// Prints a message to the screen
        /// </summary>
        /// <param name="message">The text you want to be printed.</param>
        /// <param name="line">Whether or not we add an extra newline at the end.</param>
        public static void Print(string message, bool line = true)
        {
            if (line)
                Console.WriteLine(message + Environment.NewLine);
            else
                Console.Write(message);
        }

        /// <summary>
        /// Logs a message to the console
        /// </summary>
        /// <param name="text">The message to log.</param>
        /// <param name="prefix">A prefix such as INFO or WARNING</param>
        public static void Log(string text, string prefix = "INFO")
        {
            Print("[Baackup - " + DateTime.Now.ToString("hh:mm:ss") + "] [" + prefix.ToUpper() + "] " + text);
        }

        /// <summary>
        /// Pauses the program and waits for the user to press a key
        /// </summary>
        /// <param name="message">A custom message to display the user before pausing.</param>
        public static void Pause(string message = "")
        {
            if (!(message == ""))
                Print(message);
            Console.ReadKey(true);
        }

        /// <summary>
        /// Sets the window title
        /// </summary>
        /// <param name="title"></param>
        public static void Title(string title)
        {
            Console.Title = title;
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="code">An exit code. 0 is clean exit.</param>
        public static void Exit(int code = 0)
        {
            Environment.Exit(code);
        }

        /// <summary>
        /// Clears the console window. Unused under normal use.
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Waits for a set amout of seconds
        /// </summary>
        /// <param name="seconds">The amount of seconds to wait for.</param>
        public static void Wait(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        /// <summary>
        /// Generates a new BackupID
        /// </summary>
        public static void NewBackupID()
        {
            Program.BackupID = Program.BackupSavePrefix + '_' + DateTime.Now.ToString("yyyy-mmm-dd.hh-mm-ss-ffff--backup");
        }

        #endregion
    }
}
