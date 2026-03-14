using System.Text.Json;
using ToolCore.Interfaces;
using ToolCore.Models;
using ToolCore.Services;
using ToolCore.Utilities;

namespace ToolCore.Configuration.Services;

public class ConfigurationService : IConfigurationService
{
    public AppSettings Settings { get; private set; } = new();

    public bool Load()
    {
        AppPaths.EnsureFoldersExist();

        if (!File.Exists(AppPaths.SettingsFile))
        {
            Settings = new AppSettings();
            Save();
            return true;
        }

        string json = File.ReadAllText(AppPaths.SettingsFile);

        Settings = JsonSerializer.Deserialize<AppSettings>(json)
                   ?? new AppSettings();

        return false;
    }

    public void Save()
    {
        AppPaths.EnsureFoldersExist();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(Settings, options);
        File.WriteAllText(AppPaths.SettingsFile, json);
    }
}