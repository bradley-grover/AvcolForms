namespace AvcolForms.Config.App;

internal static class Terminal
{
    private static async Task WriteLineAsync(ReadOnlyMemory<char> value, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        await Console.Out.WriteLineAsync(value);

        Console.ResetColor();
    }

    public static Task WriteErrorAsync(ReadOnlyMemory<char> value) => WriteLineAsync(value, ConsoleColor.DarkRed);
}
