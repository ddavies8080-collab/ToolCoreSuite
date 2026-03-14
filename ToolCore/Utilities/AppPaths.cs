namespace ToolCore.Utilities;

public static class AppPaths
{
    public static string AppFolder => AppContext.BaseDirectory;

    public static string AppName =>
        System.IO.Path.GetFileNameWithoutExtension(
            System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName);

    public static string DataFolder =>
        System.IO.Path.Combine(AppFolder, $"{AppName}_Data");

    public static string LogsFolder =>
        System.IO.Path.Combine(DataFolder, "Logs");

    public static string ConfigFolder =>
        System.IO.Path.Combine(DataFolder, "Config");
    public static string SettingsFile =>
    System.IO.Path.Combine(ConfigFolder, "appsettings.json");

    public static string LicensesFolder =>
        System.IO.Path.Combine(DataFolder, "Licenses");

    public static string TempFolder =>
    System.IO.Path.Combine(DataFolder, "Temp");
    public static void EnsureFoldersExist()
    {
        System.IO.Directory.CreateDirectory(DataFolder);
        System.IO.Directory.CreateDirectory(LogsFolder);
        System.IO.Directory.CreateDirectory(ConfigFolder);
        System.IO.Directory.CreateDirectory(LicensesFolder);
        System.IO.Directory.CreateDirectory(TempFolder);

    }
}