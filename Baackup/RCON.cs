﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace Baackup
{
    public class RCON
    {
        public static void Send(string command)
        {
            if (Program.UseRCON)
            {
                // We're using MCRCON by Tiiffi

                ProcessStartInfo p = new ProcessStartInfo();

                p.WindowStyle = ProcessWindowStyle.Hidden; // Hide window

                p.FileName = Program.ServerDirectory + "mcrcon.exe"; // Set the exec path

                p.Arguments = "-s -p " + Program.Config_RCON_Password + " -H " + Program.Config_RCON_Hostname + " -P " + Program.rconport + " \"" + command + '"'; // Arguments

                try
                {
                    Process.Start(p);
                }
                catch (Exception e)
                {
                    Tools.Log("Failed to launch RCON Executable: " + e.Message);
                    return;
                }

                Tools.Log("FAF RCON Command Sent: " + command);
            }
            else
            {
                Tools.Log("Failed to run RCON command: " + command + " (RCON Disabled)");
            }
        }
    }
}
