/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Config.App.Runner;

/// <summary>
/// Runs commands using the <see cref="ICommandCollection"/>
/// </summary>
public class CommandRunner
{
    private readonly ICommandCollection _commands = new CommandCollection();

    /// <summary>
    /// Runs the commands using the user input
    /// </summary>
    /// <param name="args">User input to pass in</param>
    /// <returns>A <see cref="Result"/></returns>
    public async Task<Result> RunAsync(Memory<string> args)
    {
        ArgumentNullException.ThrowIfNull(args);

        string command = args.Span[0];


        if (args.Length == 1)
        {
            if (_commands.CommandExistsWithoutParameters(command))
            {
                await _commands.RunCommandAsync(command);
                return Result.Success;
            }
            return Result.NotFound;
        }

        if (_commands.CommandExistsWithParameters(command))
        {
            await _commands.RunParamCommandAsync(command, args.Span[1..].ToArray());
            return Result.Success;
        }

        return Result.NotFound;
    }
}
