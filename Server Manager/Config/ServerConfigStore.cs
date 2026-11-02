using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Server_Manager.Config
{
    /// <summary>
    ///  Owns reading and writing servers.json. The file lives under AppPaths.BaseDir
    ///  rather than next to the executable, so it resolves independently of the working
    ///  directory and stays writable when RCC is installed under Program Files.
    /// </summary>
    public static class ServerConfigStore
    {
        private static readonly JsonSerializerOptions WriteOptions = new() { WriteIndented = true };

        public static string FilePath => Path.Combine(AppPaths.BaseDir, "servers.json");
        public static string BackupPath => Path.Combine(AppPaths.BaseDir, "servers.bak");

        public static bool Exists => File.Exists(FilePath);

        /// <summary>
        ///  First-run upgrade path: adopt a servers.json sitting next to the executable
        ///  so existing users keep their servers when the config moves to AppData.
        /// </summary>
        public static void MigrateIfNeeded()
        {
            try
            {
                if (Exists) return;

                string legacy = Path.Combine(Application.StartupPath, "servers.json");
                if (!File.Exists(legacy)) return;

                AppPaths.EnsureBaseDir();
                File.Copy(legacy, FilePath, overwrite: false);
            }
            catch
            {
                // A failed migration is not fatal: the app starts with an empty list.
            }
        }

        public static bool TryLoad(out List<ServerConfig> servers, out string? error)
        {
            servers = new List<ServerConfig>();
            error = null;

            try
            {
                if (!Exists) return true;

                var json = File.ReadAllText(FilePath);
                servers = JsonSerializer.Deserialize<List<ServerConfig>>(json) ?? new List<ServerConfig>();
                Normalize(servers);
                return true;
            }
            catch (Exception ex)
            {
                servers = new List<ServerConfig>();
                error = ex.Message;
                return false;
            }
        }

        public static bool TrySave(List<ServerConfig> servers, out string? error)
        {
            error = null;
            try
            {
                AppPaths.EnsureBaseDir();
                File.WriteAllText(FilePath, JsonSerializer.Serialize(servers, WriteOptions));
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static bool TryBackup(out string backupPath, out string? error)
        {
            backupPath = BackupPath;
            error = null;
            try
            {
                if (!Exists)
                {
                    error = "Config was not found.";
                    return false;
                }

                AppPaths.EnsureBaseDir();
                File.Copy(FilePath, BackupPath, overwrite: true);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static bool TryRestore(string sourcePath, out string? error)
        {
            error = null;
            try
            {
                AppPaths.EnsureBaseDir();
                File.Copy(sourcePath, FilePath, overwrite: true);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private static void Normalize(List<ServerConfig> servers)
        {
            for (int i = servers.Count - 1; i >= 0; i--)
            {
                var s = servers[i];
                if (s is null)
                {
                    servers.RemoveAt(i);
                    continue;
                }

                s.Name = Safe(s.Name);
                s.Host = Safe(s.Host);
                s.User = Safe(s.User);
                s.Url = Safe(s.Url);
                s.Mode = string.IsNullOrWhiteSpace(s.Mode) ? "SSH" : s.Mode.Trim();
                if (s.Port is < 1 or > 65535) s.Port = 22;
                s.IsOnline = null;
            }
        }

        private static string Safe(string? value) => value ?? "";
    }
}
