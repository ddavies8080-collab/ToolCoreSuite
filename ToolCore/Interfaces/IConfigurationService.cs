using ToolCore.Models;

namespace ToolCore.Interfaces;

public interface IConfigurationService
{
    AppSettings Settings { get; }
    bool Load();
    void Save();
}