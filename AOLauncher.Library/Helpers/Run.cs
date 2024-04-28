namespace AOLauncher.Library.Helpers;

public static class Run
{
    public static async Task<bool> WhileAsync(TimeSpan limit, Func<bool> predicate, Func<Task> taskFunc)
    {
        var start = DateTimeOffset.Now;
        while (DateTimeOffset.Now - start < limit && predicate())
            await taskFunc().ConfigureAwait(false);

        return !predicate();
    }
}