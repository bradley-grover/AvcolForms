using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Text.Json;

namespace AvcolForms.Config.App.Commands;

[Module("Database", "Commands Related to the database")]
public class DatabaseModule
{
    [Command("migrate", "migrates a provider")]
    public static async Task CreateMigrationAsync(string[] parameters)
    {
        var json = File.ReadAllText("config-appsettings.json");
        var appSettings = JsonDocument.Parse(json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
        var result = appSettings.RootElement.GetProperty("Solution-Directory").GetString();

        if (result is null)
        {
            Console.WriteLine("Solution-Directory is not configured");
            return;
        }

        var iss = InitialSessionState.CreateDefault2();

        iss.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Bypass;

        using var ps = PowerShell.Create(iss);

        string path = Path.Join(result, "sqlite-migrate.ps1");

        ps.AddScript(path)
            .AddArgument(parameters.FirstOrDefault());



        var psResults = await ps.InvokeAsync();


        var stringBuilder = new StringBuilder();


        foreach (PSObject obj in psResults)
        {
            Console.WriteLine(obj.ToString());
        }

        if (ps.HadErrors)
        {
            foreach (var error in ps.Streams.Error)
            {
                Console.WriteLine(error.Exception.Message);
            }
        }
    }
}
