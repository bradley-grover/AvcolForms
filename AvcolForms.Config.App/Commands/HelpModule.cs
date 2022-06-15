namespace AvcolForms.Config.App.Commands;

[Module("help", "lists info about other commands")]
public class HelpModule
{
    [Command("help", "lists all the available commands")]
    public async Task HelpAsync()
    {

    }
}
