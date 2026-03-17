using System.Runtime.CompilerServices;

namespace ToolCore.Interfaces;

public interface ILogService
{
    void Debug(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerFilePath] string filePath = "");

    void General(
    string message,
    [CallerMemberName] string memberName = "",
    [CallerLineNumber] int lineNumber = 0,
    [CallerFilePath] string filePath = "");

    void Information(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerFilePath] string filePath = "");

    void Warning(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerFilePath] string filePath = "");

    void Error(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerFilePath] string filePath = "");

    void Error(
        Exception ex,
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerFilePath] string filePath = "");

    void App(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerFilePath] string filePath = "");

    void CloseAndFlush();
}