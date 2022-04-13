namespace AvcolForms.Config.App.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AliasAttribute : Attribute
{
    public string[]? Aliases { get; set; }

    public AliasAttribute(params string[] aliases)
    {
        Aliases = aliases;
    }
}
