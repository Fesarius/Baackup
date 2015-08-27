# Baackup
A backup program for Minecraft servers, sponsored by sheep.

<sub>It's sponsored by sheep because it's called **baa**ckup :wink:</sub>


<img src="http://orig10.deviantart.net/8887/f/2013/346/f/1/disco_sheep_by_lockrikard-d6xo4oa.gif" alt="Disco Sheep" />


## How does I use?

1. Download one of the releases or you can build the software with Visual Studio and .NET 4.5
2. Put the contents of the "Server Files" folder into your server's root directory. (The folder with the server.properties, etc.)
3. Run the "Baackup Config.exe" that was included with the program. (This does not need to be in the server directory.)
4. Using the configuration tool, customize your settings as much as you like. We support Spigot, CraftBukkit, and Vanilla. If you are using a modded server such as Forge we DO NOT backup the "Mods" folder but, depending on your server and the mods on it, we may back up the mod config.
5. Set it to run whenever you want. If you use the plugin *Essentials*, it has [an auto backup feature](http://wiki.ess3.net/wiki/Backup). If you don't use Essentials and **do not** have the RCON feature correctly configured, make sure you have something to automatically save and toggle autosave on your server, ***otherwise your backups could become corrupted!***
6. Now just let the program do the work. If you ever need to restore from a backup just open or decompress the files, then pop them back in your server. The backups are stored as (prefix)_yyyy-mm-dd.hh-mm-ss--backup.


## Todo List:

- [x] Program Basics
- [x] Config Read / Write / Generate
- [x] Configuration Tool
- [x] Backup file transfer
- [x] Compression
- [ ] Clean up dat code
- [ ] Fix the crappy names of those variables in the Program() class.
- [ ] More error handling
- [ ] Better configuration file verification


#### *As always, we're open to pull requests.*
