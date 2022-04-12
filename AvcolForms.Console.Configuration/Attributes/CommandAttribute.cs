namespace AvcolForms.Console.Configuration.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute : Attribute
{
#nullable disable
    public string Name { get; set; }
    public string Description { get; set; }

    public CommandAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
