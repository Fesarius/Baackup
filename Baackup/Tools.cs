using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baackup
{
    public class Tools
    {
        /// <summary>
        /// Validates the configuration settings.
        /// </summary>
        /// <returns>Configuration Validity State</returns>
        public bool ValidateConfig()
        {
            try
            {
                #region RCON

                if (Program.usercon)
                {
                    if (string.IsNullOrEmpty(Program.rconpass))
                        throw new Exception("RCON Password Blank");
                    else
                        if (string.IsNullOrEmpty(Program.rconhostname))
                            { } // do something
                    else
                        if (string.IsNullOrEmpty(Program.rconport.ToString()))
                        }

                #endregion

                #region Worlds Container

                if (Program.worldscontaineractive)
                    if (string.IsNullOrEmpty(Program.worldscontainerpath))
                        throw new Exception("Worlds Container");

                #endregion
            }
            catch (Exception e)
            {
                Tools.Log("Could not verify config: " + e.Message + ". Program will terminate.", "Fatal");
                return false;
            }

            // Assuming no errors were thrown
            return true;
        }

        #region Other Stuff
        public static void Print(string message, bool line = true)
        {
            if (line)
                Console.WriteLine(message + Environment.NewLine);
            else
                Console.Write(message);
        }

        public static void Log(string text, string prefix = "INFO")
        {
            Print("[Baackup - " + DateTime.Now.ToString("hh:mm:ss") + "] [" + prefix.ToUpper() + "] " + text);
        }

        public static void Pause(string message = "")
        {
            if (!(message == ""))
                Print(message);
            Console.ReadKey(true);
        }

        public static void Title(string title)
        {
            Console.Title = title;
        }

        public static void Exit(int code = 0)
        {
            Environment.Exit(code);
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void Wait(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        public static void NewBackupID()
        {
            Program.backupid = Program.backupscustomidprefix + '_' + DateTime.Now.ToString("yyyy-mmm-dd.hh-mm-ss-ffff--backup");
        }

        #endregion
    }
}
