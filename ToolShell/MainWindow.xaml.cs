using System.Windows;
using ToolCore.Services;

namespace ToolShell.Wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TestLogButton_Click(object sender, RoutedEventArgs e)
    {
        AppServices.Logger?.Information("Test Log button clicked");
        //MessageBox.Show("Log written.");
    }

    private void DebugLogButton_Click(object sender, RoutedEventArgs e)
    {
        AppServices.Logger?.Debug("Debug Log button clicked");
//MessageBox.Show("Log written.");
    }
}