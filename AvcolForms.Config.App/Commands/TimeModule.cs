namespace AvcolForms.Config.App.Commands;

/// <summary>
/// Time module to get time information
/// </summary>
[Module("Time", "Commands relating to getting the current system time")]
internal class TimeModule
{
    /// <summary>
    /// Gets the UTC now time
    /// </summary>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    [Command("time_utc", "gets the current utc time")]
    [Alias("utc")]
    public static Task TimeAsync()
    {
        Console.WriteLine(DateTime.UtcNow);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the local time from the system
    /// </summary>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    [Command("time", "gets the current time")]
    [Alias("t")]
    public static Task TimeLocalAsync()
    {
        Console.WriteLine(DateTime.Now);
        return Task.CompletedTask;
    }
}
