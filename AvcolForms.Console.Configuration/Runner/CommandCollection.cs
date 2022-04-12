using System.Reflection;

namespace AvcolForms.Console.Configuration.Runner;

public class CommandCollection : ICommandCollection
{
    private readonly Dictionary<string, Func<Task>> _commands;
    private readonly Dictionary<string, Func<string[], Task>> _commandsWithParameters;

    public CommandCollection()
    {
        _commands = new Dictionary<string, Func<Task>>();
        _commandsWithParameters = new();
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

                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length == 0)
                {
                    var del = (Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), method);
                    _commands.Add(methodAttribute.Name.ToLower().Trim(), del);
                    continue;
                }

                var methodWithParam = (Func<string[], Task>)Delegate.CreateDelegate(typeof(Func<string[], Task>), method);

                _commandsWithParameters.Add(methodAttribute.Name.ToLower().Trim(), methodWithParam);
            }
        }

    }


    public bool CommandExistsWithoutParameters(string commandName)
    {
        return _commands.ContainsKey(commandName.ToLower().Trim());
    }

    public bool CommandExistsWithParameters(string commandName)
    {
        return _commands.ContainsKey((commandName.ToLower().Trim()));
    }

    public async Task RunCommandAsync(string commandName)
    {
        await _commands[commandName.ToLower().Trim()].Invoke();
    }

    public async Task RunParamCommandAsync(string commandName, string[] args)
    {
        await _commandsWithParameters[commandName.ToLower().Trim()].Invoke(args);
    }
}
