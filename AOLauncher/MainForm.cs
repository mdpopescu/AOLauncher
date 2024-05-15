using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using AOLauncher.Data.Services;
using AOLauncher.Library.Contracts;
using AOLauncher.Library.Models;
using AOLauncher.Library.Services;
using AOLauncher.Services;

namespace AOLauncher;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public partial class MainForm : Form, IMainUI
{
    public Server SelectedServer => rbRk5.Checked ? Server.Rk5 : Server.Rk19;
    public int ScreenCount => Screen.AllScreens.Length;

    public bool EditAccountsEnabled
    {
        set => btnEditAccounts.Enabled = value;
    }

    public AppSettings Settings
    {
        get => new(Location, Size, cbInstallations.Text);
        set
        {
            Location = value.Location;
            Size = value.Size;
            cbInstallations.SelectedIndex = cbInstallations.Items.IndexOf(value.Installation);
        }
    }

    public MainForm()
    {
        logic = new MainLogic(
            new DataLayer(AO_SETTINGS_FILE, UI_SETTINGS_FILE),
            new SafeUIDecorator(this, this),
            new SafeAORunnerDecorator(
                new AORunner(),
                new FormsLogger(tslNotification)
            )
        );

        InitializeComponent();
    }

    public void HideForm()
    {
        WindowState = FormWindowState.Minimized;
        Hide();
    }

    public void SetInstallations(IEnumerable<Installation> installations)
    {
        cbInstallations.Items.Clear();
        // ReSharper disable once CoVariantArrayConversion
        cbInstallations.Items.AddRange(installations.Select(it => it.Name).ToArray());
        cbInstallations.SelectedIndex = -1;
    }

    public bool EditInstallations(BindingList<Installation> installations)
    {
        using var form = new EditForm();
        form.Title = "Installations";
        form.DataSource = installations;
        form.HidePassword = false;
        return form.ShowDialog() == DialogResult.OK;
    }

    public void ClearAccounts()
    {
        lbAccounts.DataSource = null;
        lbAccounts.Items.Clear();
    }

    public void ShowAccounts(IEnumerable<Account> accounts)
    {
        lbAccounts.DataSource = null;
        lbAccounts.Items.Clear();
        lbAccounts.DataSource = accounts;
        lbAccounts.DisplayMember = nameof(Account.Username);
        lbAccounts.ValueMember = nameof(Account.Username);
        lbAccounts.SelectedIndex = -1;
    }


    public bool EditAccounts(BindingList<Account> accounts)
    {
        using var form = new EditForm();
        form.Title = "Accounts";
        form.DataSource = accounts;
        form.HidePassword = true;
        return form.ShowDialog() == DialogResult.OK;
    }

    public void AddContextMenu(string text, Action action)
    {
        var menu = new ToolStripMenuItem(text);
        menu.Click += (_, _) => action();
        cmsMain.Items.Insert(0, menu);
    }

    public void CenterOnScreen(int index)
    {
        var screen = Screen.AllScreens[index];

        var workingArea = screen.WorkingArea;
        Location = new Point
        {
            X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - Width) / 2),
            Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - Height) / 2),
        };

        // reset the size too
        Size = MinimumSize;
    }

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

    // ReSharper disable once AvoidAsyncVoid
    protected override async void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        await logic.SaveSettingsAsync();
    }

    //

    private const string AO_SETTINGS_FILE = "ao_settings.xml";
    private const string UI_SETTINGS_FILE = "ui_settings.txt";

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

    private void lbAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnLoginSelected.Enabled = lbAccounts.SelectedIndices.Cast<int>().Any();
    }

    private void tsmiExit_Click(object sender, EventArgs e)
    {
        Close();
    }
}