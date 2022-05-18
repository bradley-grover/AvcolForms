namespace AvcolForms.Config.App.Commands;

/// <summary>
/// Generates items randomly or determined
/// </summary>
[Module("Generater", "Generates items randomly or determined")]
public class GeneratorModule
{
    [Command("uuid", "generates a guid")]
    [Alias("guid")]
    public static async Task GenerateGuidAsync()
    {
        await GenerateGuidAsync(null!);
    }

    [Command("uuid", "generates a guid")]
    [Alias("guid")]
    public static Task GenerateGuidAsync(string[] parameters)
    {
        int number = 1;
        if (parameters is not null)
        {
            string times = parameters[0];

            if (!int.TryParse(times, out number))
            {
                Console.WriteLine("Enter a valid int for the count parameter");
                return Task.CompletedTask;
            }
        }

        for (int i = 0; i< number; i++)
        {
            Console.WriteLine(Guid.NewGuid());
        }

        return Task.CompletedTask;
    }
}
