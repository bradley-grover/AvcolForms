/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Config.App;

/// <summary>
/// Collection for commands to initialize and run
/// </summary>
public class CommandCollection : ICommandCollection
{
    private readonly Dictionary<string, Func<Task>> _commands;
    private readonly Dictionary<string, Func<string[], Task>> _commandsWithParameters;
    private readonly Dictionary<string, string> _aliases;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandCollection"/> class
    /// </summary>
    public CommandCollection()
    {
        _commands = new Dictionary<string, Func<Task>>();
        _commandsWithParameters = new();
        _aliases = new();
        Initialize();
    }

    private void Initialize()
    {
        var assembly = Assembly.GetAssembly(typeof(Program))!;

        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsClass)
            {
                continue;
            }

            var attribute = type.GetCustomAttribute<ModuleAttribute>();

            if (attribute is null)
            {
                continue;
            }

            var unfiltered = type.GetMethods();
            var methods = unfiltered.Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0);

            foreach (var method in methods)
            {
                if (!(method.ReturnType == typeof(Task)))
                {
                    throw new InvalidOperationException("Method with a Command Attribute must return a Task");
                }

                var methodAttribute = method.GetCustomAttribute<CommandAttribute>()!;
                var aliases = method.GetCustomAttribute<AliasAttribute>();

                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length == 0)
                {
                    var del = method.CreateDelegate<Func<Task>>();
                    string methodName = methodAttribute.Name.ToLower().Trim();
                    _commands.Add(methodName, del);

                    if (aliases is not null)
                    {
                        if (aliases.Aliases is not null)
                        {
                            //Array.ForEach(aliases.Aliases, alias => _aliases.Add(alias, methodName));
                        }
                    }

                    continue;
                }

                var methodWithParam = method.CreateDelegate<Func<string[],Task>>();

                var methodWithParamName = methodAttribute.Name.ToLower().Trim();
                _commandsWithParameters.Add(methodAttribute.Name.ToLower().Trim(), methodWithParam);


                if (aliases is not null)
                {
                    if (aliases.Aliases is not null)
                    {
                        //Array.ForEach(aliases.Aliases, alias => _aliases.Add(alias, methodWithParamName));
                    }
                }
            }
        }

    }

    /// <inheritdoc></inheritdoc>
    public bool CommandExistsWithoutParameters(string commandName)
    {
        return _commands.ContainsKey(commandName.ToLower().Trim());
    }

    /// <inheritdoc></inheritdoc>
    public bool CommandExistsWithParameters(string commandName)
    {
        return _commandsWithParameters.ContainsKey((commandName.ToLower().Trim()));
    }

    /// <inheritdoc></inheritdoc>
    public async Task RunCommandAsync(string commandName)
    {
        await _commands[commandName.ToLower().Trim()].Invoke();
    }

    /// <inheritdoc></inheritdoc>
    public async Task RunParamCommandAsync(string commandName, string[] args)
    {
        await _commandsWithParameters[commandName.ToLower().Trim()].Invoke(args);
    }
}
