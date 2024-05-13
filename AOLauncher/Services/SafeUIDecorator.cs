using System.ComponentModel;
using AOLauncher.Helpers;
using AOLauncher.Library.Contracts;
using AOLauncher.Library.Models;

namespace AOLauncher.Services;

internal class SafeUIDecorator(Control control, IMainUI ui) : IMainUI
{
    public Server SelectedServer => control.UIChange(() => ui.SelectedServer);

    public AppSettings Settings
    {
        get => control.UIChange(() => ui.Settings)!;
        set => control.UIChange(() => ui.Settings = value);
    }

    public bool EditAccountsEnabled
    {
        set => control.UIChange(() => ui.EditAccountsEnabled = value);
    }

    public void HideForm() =>
        control.UIChange(ui.HideForm);

    public void SetInstallations(IEnumerable<Installation> installations) =>
        control.UIChange(() => ui.SetInstallations(installations));

    public bool EditInstallations(BindingList<Installation> installations) =>
        control.UIChange(() => ui.EditInstallations(installations));

    public void ClearAccounts() =>
        control.UIChange(ui.ClearAccounts);

    public void ShowAccounts(IEnumerable<Account> accounts) =>
        control.UIChange(() => ui.ShowAccounts(accounts));

    public bool EditAccounts(BindingList<Account> accounts) =>
        control.UIChange(() => ui.EditAccounts(accounts));
}