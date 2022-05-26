using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Text.Json;

namespace AvcolForms.Config.App.Commands;

[Module("Database", "Commands Related to the database")]
public class DatabaseModule
{
    private const string Usage = "migrate [provider] [migration-name]";

    [Command("migrate", "migrates a provider")]
    public static async Task CreateMigrationAsync(string[] parameters)
    {
        if (!ValidateParameters(parameters, out var err))
        {
            Console.WriteLine(err);
            return;
        }

        string provider = parameters[0];
        string migrationName = parameters[1];

        var json = File.ReadAllText("config-appsettings.json");
        var appSettings = JsonDocument.Parse(json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
        var result = appSettings.RootElement.GetProperty("Solution-Directory").GetString();

        if (result is null)
        {
            Console.WriteLine("Solution-Directory is not configured");
            return;
        }

        string path;

        switch (provider.ToLower())
        {
            case "sqlite":
                path = Path.Join(result, "sqlite-migrate.ps1");
                break;
            case "sqlserver":
                path = Path.Join(result, "sqlserver-migrate.ps1");
                break;
            case "postgres":
                path = Path.Join(result, "postgres-migrate.ps1");
                break;
            default:
                Console.WriteLine($"{provider} is not a valid provider");
                return;
        }

        var iss = InitialSessionState.CreateDefault2();


        iss.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Bypass;

        using var ps = PowerShell.Create(iss);

        ps.Runspace.SessionStateProxy.Path.SetLocation(result);

        PSDataCollection<PSObject> output = new();

        output.DataAdded += Output_DataAdded;

        ps.AddCommand(path)
            .AddArgument(parameters.FirstOrDefault());

        var res = ps.BeginInvoke<PSObject, PSObject>(null, output);

        res.AsyncWaitHandle.WaitOne();
    }


    private static bool ValidateParameters(string[] parameters, out string errorMessage)
    {
        if (parameters is null)
        {
            errorMessage = $"Command should have paramters: {Usage}";
            return false;
        }

        if (parameters.FirstOrDefault() is null)
        {
            errorMessage = $"Command requires provider: {Usage}";
            return false;
        }

        if (!(parameters.Length == 2))
        {
            errorMessage = $"Command requires 2 parameters: {Usage}";
            return false;
        }

        if (parameters[1] is null)
        {
            errorMessage = $"Command requires migration name: {Usage}";
            return false;
        }

        errorMessage = string.Empty;

        return true;
    }

    private static void Output_DataAdded(object? sender, DataAddedEventArgs e)
    {
        PSObject newRecord = ((PSDataCollection<PSObject>)sender!)[e.Index];
        Console.WriteLine(newRecord);
    }
}
