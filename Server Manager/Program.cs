using Server_Manager.Config;

namespace Server_Manager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            ServerConfigStore.MigrateIfNeeded();

            Application.Run(new Form1());
        }
    }
}
