using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Baackup
{
    public class IOStatus
    {
        public static bool FileExists(string filepath, bool isRelative)
        {
            if (isRelative)
            {
                if (File.Exists(Program.ExecutablePath + filepath))
                    return true;
                else
                    return false;
            }
            else if (File.Exists(filepath))
                return true;
            else
                return false;
        }

        public static bool FolderExists(string folderpath, bool isRelative)
        {
            if (isRelative)
            {
                if (Directory.Exists(Program.ExecutablePath + folderpath))
                    return true;
                else
                    return false;
            }
            else if (Directory.Exists(folderpath))
                return true;
            else
                return false;
        }
    }
}
