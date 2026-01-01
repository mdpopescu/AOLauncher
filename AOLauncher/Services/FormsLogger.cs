using AOLauncher.Library.Contracts;

namespace AOLauncher.Services;

internal class FormsLogger(ToolStripItem label) : ILogger
{
    public void Log(string message)
    {
        try
        {
            label.Text = message;
        }
        catch
        {
            // do nothing
        }
    }
}