namespace AvcolForms.Console.Configuration.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ModuleAttribute : Attribute
{
#nullable disable
    public string Name { get; set; }
    public string Description { get; set; }

    public ModuleAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
