using AOLauncher.Library.Models;

namespace AOLauncher.Library.Contracts;

public interface IAORunner
{
    Task RunAsync(Installation installation, Server server, params Account[] accounts);
}