using Serilog;
using Serilog.Core;
using ToolCore.Interfaces;
using ToolCore.Models;
using ToolCore.Logging.Models;
using ToolCore.Logging.Services;

namespace ToolCore.Logging.Services;

public class LogService : ILogService
{
    private readonly ILogEventStream _logEventStream;
    public LogService(string logFilePath, AppSettings settings, ILogEventStream logEventStream)
    {
        _logEventStream = logEventStream;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(
                path: logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: settings.LogRetentionDays,
                fileSizeLimitBytes: settings.MaxLogFileSizeMB * 1024 * 1024,
                rollOnFileSizeLimit: true,
                outputTemplate:
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{LogType}] ({File}) *{Class}.{Member}:{Line}* - {Message}{NewLine}{Exception}",
                shared: true)
            .CreateLogger();

    }
        
    private Serilog.ILogger CreateContext(
        string logType,
        string memberName,
        int lineNumber,
        string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        string className = Path.GetFileNameWithoutExtension(filePath);

        return Log.ForContext("LogType", logType)
                  .ForContext("Member", memberName)
                  .ForContext("Line", lineNumber)
                  .ForContext("File", fileName)
                  .ForContext("Class", className);
    }
    private void PublishLiveEntry(
    string logType,
    string message,
    string memberName,
    int lineNumber,
    string filePath,
    Exception? exception = null)
    {
        string fileName = Path.GetFileName(filePath);
        string className = Path.GetFileNameWithoutExtension(filePath);

        var entry = new LiveLogEntry
        {
            Timestamp = DateTimeOffset.Now,
            LogType = logType,
            Category = className,
            Message = message,
            ExceptionText = exception?.ToString()
        };

        _logEventStream.Publish(entry);
    }

    public void General(string message, string memberName = "", int lineNumber = 0, string filePath = "")
    {
        CreateContext("GEN", memberName, lineNumber, filePath)
            .Information(message);

        PublishLiveEntry("GEN", message, memberName, lineNumber, filePath);
    }
    public void Debug(string message, string memberName = "", int lineNumber = 0, string filePath = "")
    {
        CreateContext("DEB", memberName, lineNumber, filePath)
            .Debug(message);

        PublishLiveEntry("DEB", message, memberName, lineNumber, filePath);
    }

    public void Information(string message, string memberName = "", int lineNumber = 0, string filePath = "")
    {
        CreateContext("INF", memberName, lineNumber, filePath)
            .Information(message);

        PublishLiveEntry("INF", message, memberName, lineNumber, filePath);
    }

    public void Warning(string message, string memberName = "", int lineNumber = 0, string filePath = "")
    {
        CreateContext("WAR", memberName, lineNumber, filePath)
            .Warning(message);

        PublishLiveEntry("WAR", message, memberName, lineNumber, filePath);
    }

    public void Error(string message, string memberName = "", int lineNumber = 0, string filePath = "")
    {
        CreateContext("ERR", memberName, lineNumber, filePath)
            .Error(message);

        PublishLiveEntry("ERR", message, memberName, lineNumber, filePath);
    }

    public void Error(Exception ex, string message, string memberName = "", int lineNumber = 0, string filePath = "")
    {
        CreateContext("ERR", memberName, lineNumber, filePath)
            .Error(ex, message);

        PublishLiveEntry("ERR", message, memberName, lineNumber, filePath, ex);
    }

    public void App(string message, string memberName = "", int lineNumber = 0, string filePath = "")
    {
        CreateContext("APP", memberName, lineNumber, filePath)
            .Verbose(message);

        PublishLiveEntry("APP", message, memberName, lineNumber, filePath);
    }
    public void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }

}