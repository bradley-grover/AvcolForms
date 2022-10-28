/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Config.App;

/// <summary>
/// Program class for the entry point of <see cref="Main"/>
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point for console config app
    /// </summary>
    /// <returns>A <see cref="Task"/></returns>
    public static async Task Main()
    {
        Console.Title = typeof(Program).Assembly.FullName!;

        Console.WriteLine("Welcome to AvcolForms Configuration App");
        Console.WriteLine("This is mainly to be used as a development application but can be used for other things as well");
        Console.WriteLine("Type 'help' to get a command overview");

        CommandRunner runner = new();

        Prompt prompt = new(configuration: 
            new PromptConfiguration
            (prompt: new PrettyPrompt.Highlighting.FormattedString(">> ")
            ));

        while (true)
        {
            string input = (await prompt.ReadLineAsync()).Text;

            Memory<string> inputArray = input.Split(' ');

            var result = await runner.RunAsync(inputArray);

            if (result is Result.NotFound)
            {
                if (inputArray.Span.IsEmpty || inputArray.Span.Length == 0)
                {
                    continue;
                }

                string element = inputArray.Span[0];

                if (string.IsNullOrEmpty(element))
                {
                    await Terminal.WriteErrorAsync("Input was invalid".AsMemory());
                    continue;
                }

                await Terminal.WriteErrorAsync($"'{element}' is not a recognized command, type 'help' to get displayed help information for commands".AsMemory());
            }
        }
    }
}
