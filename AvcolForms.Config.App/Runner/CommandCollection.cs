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
        var assembly = typeof(CommandCollection).Assembly;

        IEnumerable<Type> types = assembly.GetTypes().Where(x => x.IsClass && x.GetCustomAttribute<ModuleAttribute>() is not null);

        foreach (var type in types)
        {
            IEnumerable<MethodInfo> methods = type.GetMethods().Where(m => m.GetCustomAttribute<CommandAttribute>() is not null);

            AddMethods(methods);
        }
    }

    private void AddMethods(IEnumerable<MethodInfo> methods)
    {
        foreach (var method in methods)
        {
            AddMethod(method);
        }
    }


    private void AddMethod(MethodInfo method)
    {
        if (method.ReturnType != typeof(Task))
        {
            return;
        }

        var methodAttribute = method.GetCustomAttribute<CommandAttribute>()!;

        var methodParameters = method.GetParameters();

        if (methodParameters.Length == 0)
        {
            var @delegate = method.CreateDelegate<Func<Task>>();

            string methodName = methodAttribute.Name.ToLower().Trim();

            _commands.Add(methodName, @delegate);

            var aliases = method.GetCustomAttribute<AliasAttribute>();

            if (aliases is null)
            {
                return;
            }

            Array.ForEach(aliases.Aliases, element => _aliases.TryAdd(element, methodName));

            return;
        }
        else
        {
            var methodWithParam = method.CreateDelegate<Func<string[], Task>>();

            var methodWithParamName = methodAttribute.Name.ToLower().Trim();

            _commandsWithParameters.Add(methodAttribute.Name.ToLower().Trim(), methodWithParam);

            var aliases = method.GetCustomAttribute<AliasAttribute>();

            if (aliases is null)
            {
                return;
            }

            Array.ForEach(aliases.Aliases, element => _aliases.TryAdd(element, methodWithParamName));
        }
    }

    /// <inheritdoc></inheritdoc>
    public bool CommandExistsWithoutParameters(string commandName)
    {
        return _commands.ContainsKey(commandName.ToLower().Trim())
            || _aliases.ContainsKey(commandName.ToLower().Trim());
    }

    /// <inheritdoc></inheritdoc>
    public bool CommandExistsWithParameters(string commandName)
    {
        return _commandsWithParameters.ContainsKey((commandName.ToLower().Trim()))
            || _aliases.ContainsKey(commandName.ToLower().Trim());
    }

    /// <inheritdoc></inheritdoc>
    public async Task RunCommandAsync(string commandName)
    {
        if (_aliases.TryGetValue(commandName.ToLower().Trim(), out var alias))
        {
            await _commands[alias].Invoke();
            return;
        }

        await _commands[commandName.ToLower().Trim()].Invoke();
    }

    /// <inheritdoc></inheritdoc>
    public async Task RunParamCommandAsync(string commandName, string[] args)
    {
        if (_aliases.TryGetValue(commandName.ToLower().Trim(), out var alias))
        {
            await _commandsWithParameters[alias].Invoke(args);
            return;
        }

        await _commandsWithParameters[commandName.ToLower().Trim()].Invoke(args);
    }
}
