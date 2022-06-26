/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text.Json;
using System.Text.Json.Nodes;
using AvcolForms.Core.Data;

namespace AvcolForms.Config.App.Commands;

/// <summary>
/// Database module to group database related commands
/// </summary>
[Module("Database", "Commands Related to the database")]
public class DatabaseModule
{
    private const string Usage = "migrate [provider] [migration-name]";

    /// <summary>
    /// Migrates the specified database provider
    /// </summary>
    /// <param name="parameters">Parameters to be passed in to get the database provider</param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
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

        if (!TryGetWorkingDirectory(out var result))
        {
            Console.WriteLine("Couldn't get working directory");
            return;
        }


        string webConfigurationPath = Path.Join(result, "/AvcolForms.Web/appsettings.json");


        var webJson = File.ReadAllText(webConfigurationPath);

        var node = JsonNode.Parse(webJson);

        if (node is null)
        {
            Console.WriteLine("Failed to pass frontend web config check AvcolForms.Web/");
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


        node[Providers.DbProviderKey] = Enum.GetName(Providers.GetProvider(provider)!.Value);

        await File.WriteAllTextAsync(webConfigurationPath, JsonSerializer.Serialize(node, options: new()
        {
            WriteIndented = true,
        }));


        RunPowershellScript(path, result, migrationName);
    }

    private static bool TryGetWorkingDirectory([NotNullWhen(true)] out string? value)
    {
        var json = File.ReadAllText("config-appsettings.json");

        var appSettings = JsonDocument.Parse(json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });

        value = appSettings.RootElement.GetProperty("Solution-Directory").GetString();

        return value is not null;
    }


    private static void RunPowershellScript(string path, string dir, string migrationName)
    {
        var iss = InitialSessionState.CreateDefault2();


        iss.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Bypass;

        using var ps = PowerShell.Create(iss);

        ps.Runspace.SessionStateProxy.Path.SetLocation(dir);

        PSDataCollection<PSObject> output = new();

        output.DataAdded += Output_DataAdded;

        ps.AddCommand(path)
            .AddArgument(migrationName);

        var res = ps.BeginInvoke<PSObject, PSObject>(null, output);

        res.AsyncWaitHandle.WaitOne();
    }


    private static bool ValidateParameters(string[] parameters, [NotNullWhen(true)] out string errorMessage)
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

        if (!Providers.IsSupportedProvider(parameters[0]))
        {
            errorMessage = $"Provider is not supported";
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
