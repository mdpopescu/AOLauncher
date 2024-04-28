using System.Diagnostics;
using AOLauncher.Library.Contracts;
using AOLauncher.Library.Helpers;
using AOLauncher.Library.Models;
using WindowsInput;
using WindowsInput.Native;

namespace AOLauncher.Services;

public class AORunner : IAORunner
{
    public async Task RunAsync(Installation installation, params Account[] accounts)
    {
        foreach (var account in accounts)
        {
            using var ao = StartAO(installation);
            await DetectWindowOpenedAsync(ao).ConfigureAwait(false);
            await LoginAsync(account).ConfigureAwait(false);

            await Task.Delay(1000).ConfigureAwait(false);
        }
    }

    //

    private const uint PIXEL_COLOR = 0x00596566;
    private const int KEY_PRESS_DELAY_TIME = 100;

    private static readonly TimeSpan LIMIT = TimeSpan.FromMinutes(1);

    private readonly InputSimulator input = new();

    private static Process StartAO(Installation installation)
    {
        Process? ao = null;
        try
        {
            ao = new Process();
            ao.StartInfo.FileName = Path.Combine(installation.Path, "AnarchyOnline.exe");
            ao.StartInfo.WorkingDirectory = installation.Path;
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
        if (!await Run.WhileAsync(LIMIT, () => proc.MainWindowHandle == 0, RefreshProcAsync).ConfigureAwait(false))
            throw new Exception("Failed to start AO.");

        var hWnd = proc.MainWindowHandle;
        Win32.SetForegroundWindow(hWnd);

        uint color = 0;

        var hdc = Win32.GetDC(hWnd);
        try
        {
            if (!await Run.WhileAsync(LIMIT, () => color != PIXEL_COLOR, GetPixelAsync).ConfigureAwait(false))
                throw new Exception("Failed to start AO.");
        }
        finally
        {
            Win32.ReleaseDC(hWnd, hdc);
        }

        return;

        async Task RefreshProcAsync()
        {
            await Task.Delay(100).ConfigureAwait(false);
            proc.Refresh();
        }

        async Task GetPixelAsync()
        {
            await Task.Delay(100).ConfigureAwait(false);
            color = Win32.GetPixel(hdc, 100, 100);
        }
    }

    private async Task LoginAsync(Account acct)
    {
        input.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
        await Task.Delay(KEY_PRESS_DELAY_TIME).ConfigureAwait(false);
        input.Keyboard.KeyPress(VirtualKeyCode.HOME);
        await Task.Delay(KEY_PRESS_DELAY_TIME).ConfigureAwait(false);

        // delete existing account name entry
        for (var i = 0; i < 30; i++)
        {
            input.Keyboard.KeyPress(VirtualKeyCode.DELETE);
            await Task.Delay(1).ConfigureAwait(false);
        }

        await Task.Delay(KEY_PRESS_DELAY_TIME).ConfigureAwait(false);
        input.Keyboard.TextEntry(acct.Username);
        await Task.Delay(KEY_PRESS_DELAY_TIME).ConfigureAwait(false);
        input.Keyboard.KeyPress(VirtualKeyCode.TAB);
        await Task.Delay(KEY_PRESS_DELAY_TIME * 2).ConfigureAwait(false); // extra wait before password
        input.Keyboard.TextEntry(acct.Password);
        await Task.Delay(KEY_PRESS_DELAY_TIME).ConfigureAwait(false);
        input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
    }
}