using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AOLauncher.Models;
using AOLauncher.Services;
using WindowsInput;
using WindowsInput.Native;

namespace AOLauncher;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    //

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        data.Load(SETTINGS_FILE);
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        UpdateInstallations();
    }

    //

    private const uint PIXEL_COLOR = 0x00596566;
    private const int KEY_PRESS_DELAY_TIME = 100;
    private const string SETTINGS_FILE = "ao.xml";

    private static readonly TimeSpan LIMIT = TimeSpan.FromMinutes(1);

    private readonly InputSimulator input = new();

    private readonly DataLayer data = new();

    private void UpdateInstallations()
    {
        cbInstallations.Items.Clear();
        // ReSharper disable once CoVariantArrayConversion
        cbInstallations.Items.AddRange(data.GetInstallationNames());
        cbInstallations.SelectedIndex = -1;
    }

    private void UpdateAccounts()
    {
        if (cbInstallations.SelectedIndex < 0)
            return;

        lbAccounts.Items.Clear();
        lbAccounts.DataSource = data.GetAccounts(cbInstallations.Text);
        lbAccounts.DisplayMember = nameof(Account.Username);
        lbAccounts.ValueMember = nameof(Account.Username);
        lbAccounts.SelectedIndex = 0;
    }

    private static Process StartAO(AODetails aoDetails)
    {
        Process? ao = null;
        try
        {
            ao = new Process();
            ao.StartInfo.FileName = Path.Combine(aoDetails.Path, "AnarchyOnline.exe");
            ao.StartInfo.WorkingDirectory = aoDetails.Path;
            ao.StartInfo.UseShellExecute = false;
            ao.StartInfo.RedirectStandardInput = true;
            ao.StartInfo.Arguments = "IA700453413 IP7505 DU"; // default AO args
            ao.Start();
            return ao;
        }
        catch
        {
            ao?.Dispose();
            throw;
        }
    }

    private static async Task DetectWindowOpenedAsync(Process proc)
    {
        // do not allow this loop to run for more than one minute
        var start = DateTimeOffset.Now;
        while (proc.MainWindowHandle == 0 && DateTimeOffset.Now - start < LIMIT)
        {
            await Task.Delay(100);
            proc.Refresh();
        }

        var hWnd = proc.MainWindowHandle;
        Win32.SetForegroundWindow(hWnd);

        var hdc = Win32.GetDC(hWnd);
        try
        {
            // do not allow this loop to run for more than one minute
            start = DateTimeOffset.Now;

            uint color;
            do
            {
                color = Win32.GetPixel(hdc, 100, 100);
            }
            while (color != PIXEL_COLOR && DateTimeOffset.Now - start < LIMIT);

            if (color != PIXEL_COLOR)
                throw new Exception("Failed to start AO.");
        }
        finally
        {
            Win32.ReleaseDC(hWnd, hdc);
        }
    }

    private async Task LoginAsync(Account acct)
    {
        input.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
        await Task.Delay(KEY_PRESS_DELAY_TIME);
        input.Keyboard.KeyPress(VirtualKeyCode.HOME);
        await Task.Delay(KEY_PRESS_DELAY_TIME);

        // delete existing account name entry
        for (var i = 0; i < 30; i++)
        {
            input.Keyboard.KeyPress(VirtualKeyCode.DELETE);
            await Task.Delay(1);
        }

        await Task.Delay(KEY_PRESS_DELAY_TIME);
        input.Keyboard.TextEntry(acct.Username);
        await Task.Delay(KEY_PRESS_DELAY_TIME);
        input.Keyboard.KeyPress(VirtualKeyCode.TAB);
        await Task.Delay(KEY_PRESS_DELAY_TIME * 2); // extra wait before password
        input.Keyboard.TextEntry(acct.Password);
        await Task.Delay(KEY_PRESS_DELAY_TIME);
        input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
    }

    //

    private void btnEditInstallations_Click(object sender, EventArgs e)
    {
        using var form = new EditForm(data.InstallationsTable);
        if (form.ShowDialog() != DialogResult.OK)
            return;

        data.Save(SETTINGS_FILE);
        UpdateInstallations();
    }

    private void cbInstallations_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateAccounts();
    }

    private void btnEditAccounts_Click(object sender, EventArgs e)
    {
        using var accountsTable = data.GetAccountsTable(cbInstallations.Text);
        using var form = new EditForm(accountsTable);
        if (form.ShowDialog() != DialogResult.OK)
            return;

        data.UpdateAccounts(cbInstallations.Text, accountsTable);

        data.Save(SETTINGS_FILE);
        UpdateAccounts();
    }

    private async void btnLoginSelected_Click(object sender, EventArgs e)
    {
        //using var ao = StartAO(aoDetails);
        //await DetectWindowOpenedAsync(ao);
        //await LoginAsync(account);

        //// final wait before processing next one (when logging in multiple accounts)
        //await Task.Delay(500);
    }
}