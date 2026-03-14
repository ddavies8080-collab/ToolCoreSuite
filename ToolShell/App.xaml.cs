using System.Windows;
using System.Windows.Threading;
using ToolShell.Bootstrapper;

namespace ToolShell.Wpf;

public partial class App : Application
{
    private readonly AppBootstrapper _bootstrapper = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _bootstrapper.OnStartup(this, e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _bootstrapper.OnExit(e);
        base.OnExit(e);
    }

    private void App_DispatcherUnhandledException(
        object sender,
        DispatcherUnhandledExceptionEventArgs e)
    {
        _bootstrapper.OnDispatcherUnhandledException(e);
    }
}