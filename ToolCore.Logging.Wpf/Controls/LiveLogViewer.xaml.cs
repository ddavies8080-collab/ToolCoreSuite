using System.Collections.ObjectModel;
using System.Windows.Controls;
using ToolCore.Logging.Models;
using ToolCore.Services;

namespace ToolCore.Logging.Wpf.Controls;

public partial class LiveLogViewer : UserControl
{
    public ObservableCollection<LiveLogEntry> Entries { get; } = new();

    public IEnumerable<string>? AllowedCategories { get; set; }

    public LiveLogViewer()
    {
        InitializeComponent();

        LogGrid.ItemsSource = Entries;

        Loaded += LiveLogViewer_Loaded;
    }

    private void LiveLogViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        LoadExistingEntries();
        SubscribeToLiveEvents();
    }

    private void LoadExistingEntries()
    {
        var snapshot = AppServices.LogEventStream?.GetSnapshot();
        if (snapshot is null)
            return;

        Entries.Clear();

        foreach (var entry in snapshot)
        {
            if (ShouldShow(entry))
            {
                Entries.Add(entry);
            }
        }
    }

    private void SubscribeToLiveEvents()
    {
        if (AppServices.LogEventStream is null)
            return;

        AppServices.LogEventStream.EntryAdded -= OnEntryAdded;
        AppServices.LogEventStream.EntryAdded += OnEntryAdded;
    }

    private void OnEntryAdded(LiveLogEntry entry)
    {
        if (!ShouldShow(entry))
            return;

        Dispatcher.Invoke(() =>
        {
            Entries.Add(entry);

            if (LogGrid.Items.Count > 0)
            {
                var lastItem = LogGrid.Items[LogGrid.Items.Count - 1];
                LogGrid.ScrollIntoView(lastItem);
            }
        });
    }

    private bool ShouldShow(LiveLogEntry entry)
    {
        if (AllowedCategories is null)
            return true;

        var allowed = AllowedCategories.ToList();
        if (allowed.Count == 0)
            return true;

        return allowed.Contains(entry.Category, StringComparer.OrdinalIgnoreCase);
    }
}