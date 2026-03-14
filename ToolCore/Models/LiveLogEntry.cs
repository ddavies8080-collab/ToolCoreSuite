namespace ToolCore.Logging.Models;

public sealed class LiveLogEntry
{
    public DateTimeOffset Timestamp { get; init; }
    public string LogType { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string? ExceptionText { get; init; }
}