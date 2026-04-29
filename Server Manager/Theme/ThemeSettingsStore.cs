using System;
using System.IO;
using System.Text.Json;
using Server_Manager.Theme;

namespace Server_Manager.Theme;

public static class ThemeSettingsStore
{
    private sealed class Dto
    {
        public ThemeMode Mode { get; set; } = ThemeMode.Light;
    }

    private static string BaseDir => Path.Combine(
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RCC - Remote Connection Center"));

    private static string FilePath => Path.Combine(BaseDir, "theme.json");

    public static ThemeMode Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return ThemeMode.Light;
            var json = File.ReadAllText(FilePath);
            var dto = JsonSerializer.Deserialize<Dto>(json);
            return dto?.Mode ?? ThemeMode.Light;
        }
        catch
        {
            return ThemeMode.Light;
        }
    }

    public static void Save(ThemeMode mode)
    {
        Directory.CreateDirectory(BaseDir);
        var dto = new Dto { Mode = mode };
        File.WriteAllText(FilePath, JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true }));
    }

}