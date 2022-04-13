namespace AvcolForms.Config.App.Commands;

[Module("Time", "Commands relating to getting the current system time")]
internal class TimeModule
{
    [Command("time_utc", "gets the current utc time")]
    [Alias("utc")]
    public static Task TimeAsync()
    {
        Console.WriteLine(DateTime.UtcNow);
        return Task.CompletedTask;
    }
    [Command("time", "gets the current time")]
    [Alias("t")]
    public static Task TimeLocalAsync()
    {
        Console.WriteLine(DateTime.Now);
        return Task.CompletedTask;
    }
}
