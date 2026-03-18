using System.Reflection;

namespace ToolShell;

public static class AppInfo
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static string ProductName =>
        Assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product
        ?? "ToolOne";

    public static string CompanyName =>
        Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company
        ?? "ToolCoreSuite";

    public static string Version =>
        Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? Assembly.GetName().Version?.ToString()
        ?? "1.0.0";

    public static string ExecutableName =>
        Assembly.GetName().Name
        ?? "ToolOne";
}