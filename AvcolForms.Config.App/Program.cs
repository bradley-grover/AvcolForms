using System.Reflection;

namespace AvcolForms.Config.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.Title = typeof(Program).Assembly.FullName!;

        CommandRunner runner = new();

        Prompt prompt = new(configuration: 
            new PromptConfiguration
            (prompt: new PrettyPrompt.Highlighting.FormattedString(">> ")
            ));
        while (true)
        {
            string input = (await prompt.ReadLineAsync()).Text;

            await runner.RunAsync(input.Split(" "));
        }
    }
}
