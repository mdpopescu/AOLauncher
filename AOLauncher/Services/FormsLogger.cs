using AOLauncher.Library.Contracts;

namespace AOLauncher.Services;

internal class FormsLogger(ToolStripItem label) : ILogger
{
    public void LogError(Exception ex)
    {
        label.Text = $"ERROR: {ex.Message}";
    }
}