using ToolCore.Interfaces;

namespace ToolCore.Services;

public static class AppServices
{
    public static ILogService? Logger { get; set; }
    public static ILogEventStream? LogEventStream { get; set; }
    public static IConfigurationService? Configuration { get; set; }
}