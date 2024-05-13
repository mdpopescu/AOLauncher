using System.ComponentModel;
using AOLauncher.Library.Contracts;
using AOLauncher.Library.Models;

namespace AOLauncher.Library.Services;

public class MainLogic(IDataLayer data, IMainUI ui, IAORunner runner)
{
    public async Task InitializeAsync()
    {
        var installations = await data.GetInstallationsAsync().ConfigureAwait(false);
        ui.SetInstallations(installations);

        ui.Settings = await data.LoadSettingsAsync().ConfigureAwait(false);
    }

    /// <summary>
    ///     Save the form location and size and the current installation.
    /// </summary>
    public async Task SaveSettingsAsync()
    {
        await data.SaveSettingsAsync(ui.Settings).ConfigureAwait(false);
    }

    public async Task EditInstallationsAsync()
    {
        var installations = await data.GetInstallationsAsync().ConfigureAwait(false);

        var model = new BindingList<Installation>(installations)
        {
            AllowNew = true,
            AllowEdit = true,
            AllowRemove = true,
        };
        if (ui.EditInstallations(model))
            await data.SaveInstallationsAsync(installations).ConfigureAwait(false);
    }

    public async Task UpdateAccountsAsync(int installationIndex)
    {
        // save the current installation
        await SaveSettingsAsync().ConfigureAwait(false);

        ui.ClearAccounts();
        ui.EditAccountsEnabled = installationIndex >= 0;

        if (installationIndex < 0)
            return;

        var installations = await data.GetInstallationsAsync().ConfigureAwait(false);
        if (installationIndex >= installations.Count)
            return;

        ui.ShowAccounts(installations[installationIndex].Accounts);
    }

    public async Task EditAccountsAsync(int installationIndex)
    {
        if (installationIndex < 0)
            return;

        var installations = await data.GetInstallationsAsync().ConfigureAwait(false);
        if (installationIndex >= installations.Count)
            return;

        var model = new BindingList<Account>(installations[installationIndex].Accounts)
        {
            AllowNew = true,
            AllowEdit = true,
            AllowRemove = true,
        };
        if (!ui.EditAccounts(model))
            return;

        await data.SaveInstallationsAsync(installations).ConfigureAwait(false);
        ui.ShowAccounts(installations[installationIndex].Accounts);
    }

    public async Task LoginAsync(int installationIndex, int[] accountIndices)
    {
        if (installationIndex < 0)
            return;

        var installations = await data.GetInstallationsAsync().ConfigureAwait(false);
        if (installationIndex >= installations.Count)
            return;

        ui.HideForm();

        var installation = installations[installationIndex];
        var accounts = installation.Accounts.Where((_, index) => accountIndices.Contains(index)).ToArray();
        await runner.RunAsync(installations[installationIndex], ui.SelectedServer, accounts).ConfigureAwait(false);
    }
}