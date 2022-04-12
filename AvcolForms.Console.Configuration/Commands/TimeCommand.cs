using AvcolForms.Console.Configuration.Attributes;
using PrettyPrompt;

namespace AvcolForms.Console.Configuration.Commands;

[Module("Time", "")]
internal class TimeCommand
{
    [Command("time_utc", "gets the current utc time")]
    public async Task TimeAsync()
    {
        System.Console.WriteLine(DateTime.UtcNow.ToString());
    }
    [Command("time", "gets the current time")]
    public async Task TimeLocalAsync()
    {

    }
}
