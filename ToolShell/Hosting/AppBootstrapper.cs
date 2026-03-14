using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ToolCore.Configuration.Services;
using ToolCore.Logging.Services;
using ToolCore.Services;
using ToolCore.Utilities;
using ToolShell.Wpf;

namespace ToolShell.Bootstrapper
{
    public sealed class AppBootstrapper
    {

        public void OnStartup(Application app, StartupEventArgs e)
        {
            //Check/Create default folders for app
            AppPaths.EnsureFoldersExist();

            //Check/Create default App Settings
            AppServices.Configuration = new ConfigurationService();
            bool createdDefaultSettings = AppServices.Configuration.Load();
            //Load Settings to Var
            var settings = AppServices.Configuration.Settings;
            //Set the LogFilePath using the App Paths master list
            string logFilePath =
                System.IO.Path.Combine(AppPaths.LogsFolder, "log-.txt");

            //Create the live log stream
            AppServices.LogEventStream = new LogEventStream(1000);
                       
            //Start the logger service using the loaded data
            AppServices.Logger = new LogService(logFilePath, settings, AppServices.LogEventStream);

            //Log Base Startup Info
            AppServices.Logger.Information("Application started.");
            if (createdDefaultSettings)
            {
                AppServices.Logger.App("Default settings file created.");
            }
            AppServices.Logger.App($"Settings loaded from: {AppPaths.SettingsFile}");
            AppServices.Logger.Error($"LogRetentionDays: {AppServices.Configuration.Settings.LogRetentionDays}");
            AppServices.Logger.Warning($"MaxLogFileSizeMB: {AppServices.Configuration.Settings.MaxLogFileSizeMB}");

            //Load Form
            var mainWindow = new MainWindow();
            //Show all Logs
            mainWindow.AppLogViewer.AllowedCategories = Array.Empty<string>();

            //Show user logs 
            //mainWindow.AppLogViewer.AllowedCategories = new[]
            //{
            //    "MainWindow.xaml"
            //};
            app.MainWindow = mainWindow;
            mainWindow.Show();
                        
        }
        public void OnExit(ExitEventArgs e) {
            //Log App Exit Info
            AppServices.Logger?.Information("Application exited.");
            AppServices.Logger?.CloseAndFlush();
         }
        public void OnDispatcherUnhandledException(
            DispatcherUnhandledExceptionEventArgs e)
        {
            //Log Complex error if software crashes
            AppServices.Logger?.Error(e.Exception, "Unhandled UI exception.");
        }
    }
}
