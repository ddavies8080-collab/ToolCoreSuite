namespace ToolCore.Models;

public class AppSettings
{
    public int LogRetentionDays { get; set; } = 14;
    public int MaxLogFileSizeMB { get; set; } = 10;
    public bool CheckForUpdatesOnStartup { get; set; } = true;
}