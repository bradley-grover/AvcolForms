namespace AvcolForms.Console.Configuration.Runner;

public class CommandRunner
{
    private readonly ICommandCollection _commands = new CommandCollection();
    public async Task<Result> RunAsync(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        string command = args[0];


        if (args.Length == 1)
        {
            if (_commands.CommandExistsWithoutParameters(command))
            {
                await _commands.RunCommandAsync(command);
                return Result.Success;
            }
            else
            {
                return Result.NotFound;
            }
        }

        if (_commands.CommandExistsWithParameters(command))
        {
            await _commands.RunParamCommandAsync(command, args.Skip(1).ToArray());
            return Result.Success;
        }
        else
        {
            return Result.NotFound;
        }
    }
}
