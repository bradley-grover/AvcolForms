namespace AvcolForms.Config.App.Runner;

/// <summary>
/// A command collection to store and run commands
/// </summary>
public interface ICommandCollection
{
    /// <summary>
    /// Checks for a command that exists without parameters with the specified name
    /// </summary>
    /// <param name="commandName">The command name</param>
    /// <returns>Whether a non-parameterised command exists with that name</returns>
    bool CommandExistsWithoutParameters(string commandName);

    /// <summary>
    /// Checks for a comamnd that exists with parameters with the specified name
    /// </summary>
    /// <param name="commandName">The command name</param>
    /// <returns>Whether a command with parameters exists with that name</returns>
    bool CommandExistsWithParameters(string commandName);

    /// <summary>
    /// Runs the specified command asynchronously
    /// </summary>
    /// <param name="commandName">The command name</param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task RunCommandAsync(string commandName);

    /// <summary>
    /// Runs the specified command with parameters asynchronously
    /// </summary>
    /// <param name="commandName">The command name</param>
    /// <param name="args"></param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task RunParamCommandAsync(string commandName, string[] args);
}
