using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ToolCore.Logging.Models;
using ToolCore.Services;

namespace ToolCore.Logging.Wpf.Controls;

public partial class LiveLogViewer : UserControl
{
    public ObservableCollection<LiveLogEntry> Entries { get; } = new();

    public IEnumerable<string>? AllowedLogTypes { get; set; }

    public LiveLogViewer()
    {
        InitializeComponent();
        InitializeComponent();
        UpdateTimeColumnVisibility();

        LogGrid.ItemsSource = Entries;

        Loaded += LiveLogViewer_Loaded;

    }

    public bool ShowTimeColumn
    {
        get => (bool)GetValue(ShowTimeColumnProperty);
        set => SetValue(ShowTimeColumnProperty, value);
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
        if (AllowedLogTypes is null)
            return true;

        var allowed = AllowedLogTypes.ToList();
        if (allowed.Count == 0)
            return true;

        return allowed.Contains(entry.LogType, StringComparer.OrdinalIgnoreCase);
    }
    private void LogGrid_LostFocus(object sender, RoutedEventArgs e)
    {
        LogGrid.UnselectAll();
    }
    
    
    public static readonly DependencyProperty ShowTimeColumnProperty =
    DependencyProperty.Register(
        nameof(ShowTimeColumn),
        typeof(bool),
        typeof(LiveLogViewer),
        new PropertyMetadata(true, OnShowTimeColumnChanged));

    private static void OnShowTimeColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is LiveLogViewer viewer)
        {
            viewer.UpdateTimeColumnVisibility();
        }
    }
    private void UpdateTimeColumnVisibility()
    {
        if (TimeColumn != null)
        {
            TimeColumn.Visibility = ShowTimeColumn
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
private void LogGrid_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
{
    if (e.Item == null)
        return;

    // Replace LiveLogEntry with your actual row model type
    if (e.Item is LiveLogEntry entry)
    {
        e.ClipboardRowContent.Clear();

        e.ClipboardRowContent.Add(
            new DataGridClipboardCellContent(
                e.Item,
                null,
                entry.LogTypeClipboard ?? string.Empty));

        e.ClipboardRowContent.Add(
            new DataGridClipboardCellContent(
                e.Item,
                null,
                entry.Timestamp.ToString("HH:mm:ss.fff")));

        e.ClipboardRowContent.Add(
            new DataGridClipboardCellContent(
                e.Item,
                null,
                entry.Message ?? string.Empty));
    }
}
}