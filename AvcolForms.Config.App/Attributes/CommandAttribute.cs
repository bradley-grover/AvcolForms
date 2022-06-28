/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Config.App.Attributes;

/// <summary>
/// Module information for a command in the terminal
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CommandAttribute : Attribute
{
#nullable disable
    /// <summary>
    /// The name of the command, this is used in reflection so we can execute the command
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Description about what the command performs
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandAttribute"/> class with the specified name and description
    /// </summary>
    /// <param name="name">Command name</param>
    /// <param name="description">Command description</param>
    public CommandAttribute(string name, string description)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(description, nameof(description));

        Name = name;
        Description = description;
    }
}
