namespace AOLauncher.Helpers;

internal static class UIExtensions
{
    /// <summary>
    ///     Use to safely perform an action on a control from a background thread
    /// </summary>
    /// <param name="control"> Control to check </param>
    /// <param name="action"> Action that should involve that control (and ONLY that one) </param>
    public static void UIChange(this Control control, Action action)
    {
        try
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }
        catch (InvalidOperationException)
        {
            // ignore, the control has not yet been created or has been disposed
        }
    }

    /// <summary>
    ///     Use to safely invoke a function on a control from a background thread
    /// </summary>
    /// <param name="control">Control to check.</param>
    /// <param name="func">Function that should involve that control (and ONLY that one).</param>
    public static T? UIChange<T>(this Control control, Func<T> func)
    {
        try
        {
            return control.InvokeRequired ? control.Invoke(func) : func();
        }
        catch (InvalidOperationException)
        {
            // the control has not yet been created or has been disposed
            return default;
        }
    }
}