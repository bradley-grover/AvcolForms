
namespace AvcolForms.Console.Configuration.Runner;

public interface ICommandCollection
{
    bool CommandExistsWithoutParameters(string commandName);
    bool CommandExistsWithParameters(string commandName);
    Task RunCommandAsync(string commandName);
    Task RunParamCommandAsync(string commandName, string[] args);
}
