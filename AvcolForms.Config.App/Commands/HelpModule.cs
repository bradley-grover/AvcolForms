/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Config.App.Commands;

/// <summary>
/// Help module for list help information on the terminal
/// </summary>
[Module("Help", "lists info about other commands")]
public class HelpModule
{
    [Command("help", "lists all the available commands")]
    public static Task HelpAsync()
    {
        return Task.CompletedTask; // TODO: Make help command
    }
}
