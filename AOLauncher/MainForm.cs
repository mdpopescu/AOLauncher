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
    public Server SelectedServer => rbRk5.Checked ? Server.Rk5 : Server.Rk19;

    public bool EditAccountsEnabled
    {
        set => this.UIChange(() => btnEditAccounts.Enabled = value);
    }

    public bool LoginEnabled
    {
        set => this.UIChange(() => btnLoginSelected.Enabled = value);
    }

    public MainForm()
    {
        InitializeComponent();

        logic = new MainLogic(
            new DataLayer(SETTINGS_FILE),
            this,
            new AORunner()
        );
    }

    public void HideForm() => this.UIChange(
        () =>
        {
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
        }
    );

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

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (WindowState == FormWindowState.Minimized)
            Hide();
    }

    //

    private const string SETTINGS_FILE = "ao.xml";

    private readonly MainLogic logic;

    private void RestoreForm()
    {
        WindowState = FormWindowState.Minimized;
        Show();
        WindowState = FormWindowState.Normal;
    }

    //

    [SuppressMessage("Usage", "IDE1006", Justification = "Company convention for event handlers")]
    private async void btnEditInstallations_Click(object sender, EventArgs e)
    {
        await logic.EditInstallationsAsync();
    }

    [SuppressMessage("Usage", "IDE1006", Justification = "Company convention for event handlers")]
    private async void cbInstallations_SelectedIndexChanged(object sender, EventArgs e)
    {
        await logic.UpdateAccountsAsync(cbInstallations.SelectedIndex);
    }

    [SuppressMessage("Usage", "IDE1006", Justification = "Company convention for event handlers")]
    private async void btnEditAccounts_Click(object sender, EventArgs e)
    {
        await logic.EditAccountsAsync(cbInstallations.SelectedIndex);
    }

    [SuppressMessage("Usage", "IDE1006", Justification = "Company convention for event handlers")]
    private async void btnLoginSelected_Click(object sender, EventArgs e)
    {
        await logic.LoginAsync(cbInstallations.SelectedIndex, lbAccounts.SelectedIndices.Cast<int>().ToArray());
    }

    [SuppressMessage("Usage", "IDE1006", Justification = "Company convention for event handlers")]
    private void niMain_DoubleClick(object sender, EventArgs e)
    {
        RestoreForm();
    }

    [SuppressMessage("Usage", "IDE1006", Justification = "Company convention for event handlers")]
    private async void lbAccounts_DoubleClick(object sender, EventArgs e)
    {
        await logic.LoginAsync(cbInstallations.SelectedIndex, lbAccounts.SelectedIndices.Cast<int>().ToArray());
    }
}