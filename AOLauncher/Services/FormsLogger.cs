using AOLauncher.Library.Contracts;

namespace AOLauncher.Services;

internal class FormsLogger(ToolStripItem label) : ILogger
{
    public void Log(string message)
    {
        label.Text = message;
    }
}