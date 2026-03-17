using System.Windows;
using Serilog;
using ToolCore.Interfaces;
using ToolCore.Services;

namespace ToolShell.Wpf;

public partial class MainWindow : Window
{
    private static ILogService? _log => AppServices.Logger;
    public static class AppEnvironment
    {
#if DEBUG
        public static bool DevelopmentMode => true;
#else
    public static bool DevelopmentMode => false;
#endif
    }
    public MainWindow()
    {
        InitializeComponent();
    }

    private void GeneralLogButton_Click(object sender, RoutedEventArgs e)
    {
        _log?.General("Genera Log button clicked");
           }

    private void WarningLogButton_Click(object sender, RoutedEventArgs e)
    {
        _log?.Warning("Warning Log button clicked");
    }
    private void ErrorLogButton_Click(object sender, RoutedEventArgs e)
    {
        _log?.Error("Error Log button clicked");
    }
}