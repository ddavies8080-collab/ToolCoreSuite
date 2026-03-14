using ToolCore.Logging.Models;
using ToolCore.Interfaces;
using ToolCore.Models;

namespace ToolCore.Logging.Services;

public sealed class LogEventStream : ILogEventStream
{
    private readonly List<LiveLogEntry> _entries = new();
    private readonly object _lock = new();
    private readonly int _maxEntries;

    public event Action<LiveLogEntry>? EntryAdded;

    public LogEventStream(int maxEntries = 1000)
    {
        _maxEntries = maxEntries;
    }

    public IReadOnlyList<LiveLogEntry> GetSnapshot()
    {
        lock (_lock)
        {
            return _entries.ToList();
        }
    }

    public void Publish(LiveLogEntry entry)
    {
        lock (_lock)
        {
            _entries.Add(entry);

            if (_entries.Count > _maxEntries)
            {
                _entries.RemoveAt(0);
            }
        }

        EntryAdded?.Invoke(entry);
    }
}