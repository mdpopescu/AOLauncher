using AOLauncher.Library.Models;
using System.ComponentModel;

namespace AOLauncher.Library.Contracts;

public interface IMainUI
{
    Server SelectedServer { get; }
    int ScreenCount { get; }

    bool EditAccountsEnabled { set; }

    AppSettings Settings { get; set; }

    void HideForm();

    void SetInstallations(IEnumerable<Installation> installations);
    bool EditInstallations(BindingList<Installation> installations);

    void ClearAccounts();
    void ShowAccounts(IEnumerable<Account> accounts);

    bool EditAccounts(BindingList<Account> accounts);

    void AddContextMenu(string text, Action action);
    void CenterOnScreen(int index);
}