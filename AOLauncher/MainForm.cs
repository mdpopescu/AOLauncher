using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using AOLauncher.Data.Services;
using AOLauncher.Helpers;
using AOLauncher.Library.Contracts;
using AOLauncher.Library.Models;
using AOLauncher.Library.Services;
using AOLauncher.Services;

namespace AOLauncher;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public partial class MainForm : Form, IMainUI
{
    public bool EditAccountsEnabled
    {
        set => this.UIChange(() => btnEditAccounts.Enabled = value);
    }

    public MainForm()
    {
        InitializeComponent();

        logic = new MainLogic(data, this);
    }

    public void SetInstallations(IEnumerable<Installation> installations) => this.UIChange(
        () =>
        {
            cbInstallations.Items.Clear();
            // ReSharper disable once CoVariantArrayConversion
            cbInstallations.Items.AddRange(installations.Select(it => it.Name).ToArray());
            cbInstallations.SelectedIndex = -1;
        }
    );

    public bool EditInstallations(BindingList<Installation> installations) => this.UIChange(
        () =>
        {
            using var form = new EditForm();
            form.Title = "Installations";
            form.DataSource = installations;
            return form.ShowDialog() == DialogResult.OK;
        }
    );

    public void ClearAccounts() => this.UIChange(
        () =>
        {
            lbAccounts.DataSource = null;
            lbAccounts.Items.Clear();
        }
    );

    public void ShowAccounts(IEnumerable<Account> accounts) => this.UIChange(
        () =>
        {
            lbAccounts.DataSource = null;
            lbAccounts.Items.Clear();
            lbAccounts.DataSource = accounts;
            lbAccounts.DisplayMember = nameof(Account.Username);
            lbAccounts.ValueMember = nameof(Account.Username);
            lbAccounts.SelectedIndex = -1;
        }
    );

    public bool EditAccounts(BindingList<Account> accounts) => this.UIChange(
        () =>
        {
            using var form = new EditForm();
            form.Title = "Accounts";
            form.DataSource = accounts;
            return form.ShowDialog() == DialogResult.OK;
        }
    );

    //

    // ReSharper disable once AvoidAsyncVoid
    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);

        await logic.InitializeAsync();
    }

    //

    private const string SETTINGS_FILE = "ao.xml";

    private readonly DataLayer data = new(SETTINGS_FILE);
    private readonly AORunner runner = new();

    private readonly MainLogic logic;

    //

    private async void btnEditInstallations_Click(object sender, EventArgs e)
    {
        await logic.EditInstallationsAsync();
    }

    private async void cbInstallations_SelectedIndexChanged(object sender, EventArgs e)
    {
        await logic.UpdateAccountsAsync(cbInstallations.SelectedIndex);
    }

    private async void btnEditAccounts_Click(object sender, EventArgs e)
    {
        await logic.EditAccountsAsync(cbInstallations.SelectedIndex);
    }

    private async void btnLoginSelected_Click(object sender, EventArgs e)
    {
        //await runner.RunAsync(installation, account);

        //using var ao = StartAO(aoDetails);
        //await DetectWindowOpenedAsync(ao);
        //await LoginAsync(account);

        //// final wait before processing next one (when logging in multiple accounts)
        //await Task.Delay(500);
    }
}