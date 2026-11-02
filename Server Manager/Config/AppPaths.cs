using System;
using System.IO;

namespace Server_Manager.Config
{
    public static class AppPaths
    {
        public static string BaseDir => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "RCC - Remote Connection Center");

        public static void EnsureBaseDir() => Directory.CreateDirectory(BaseDir);
    }
}
