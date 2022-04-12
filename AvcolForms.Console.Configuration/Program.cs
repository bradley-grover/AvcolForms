#region GlobalUsings
global using AvcolForms.Console.Configuration.Attributes;
global using AvcolForms.Console.Configuration.Runner;
#endregion

using PrettyPrompt;


namespace AvcolForms.Console.Configuration;

public class Program
{
    public static async Task Main(string[] args)
    {
        CommandRunner runner = new();

        Prompt prompt = new();

        while (true)
        {
            string input = (await prompt.ReadLineAsync()).Text;

            await runner.RunAsync(input.Split(" "));
        }
    }
}
