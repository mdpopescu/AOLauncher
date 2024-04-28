using AOLauncher.Library.Models;
using System.ComponentModel;

namespace AOLauncher.Library.Contracts;

public interface IMainUI
{
    bool EditAccountsEnabled { set; }
    bool LoginEnabled { set; }

    void SetInstallations(IEnumerable<Installation> installations);
    bool EditInstallations(BindingList<Installation> installations);

    void ClearAccounts();
    void ShowAccounts(IEnumerable<Account> accounts);

    bool EditAccounts(BindingList<Account> accounts);
}