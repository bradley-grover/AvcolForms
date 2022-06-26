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
