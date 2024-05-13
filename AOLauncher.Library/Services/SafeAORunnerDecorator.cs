using AOLauncher.Library.Contracts;
using AOLauncher.Library.Models;

namespace AOLauncher.Library.Services;

public class SafeAORunnerDecorator(IAORunner runner, ILogger logger) : IAORunner
{
    public async Task RunAsync(Installation installation, Server server, params Account[] accounts)
    {
        try
        {
            await runner.RunAsync(installation, server, accounts).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.Log($"ERROR trying to launch [{installation.Name}] from path [{installation.Path}]: {ex}");
        }
    }
}