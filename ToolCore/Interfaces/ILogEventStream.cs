using ToolCore.Logging.Models;

namespace ToolCore.Interfaces;

public interface ILogEventStream
{
    event Action<LiveLogEntry>? EntryAdded;

    IReadOnlyList<LiveLogEntry> GetSnapshot();

    void Publish(LiveLogEntry entry);
}