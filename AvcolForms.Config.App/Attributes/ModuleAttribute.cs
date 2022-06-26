namespace AvcolForms.Config.App.Attributes;

/// <summary>
/// Module to group together methods marked with <see cref="CommandAttribute"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ModuleAttribute : Attribute
{
#nullable disable
    /// <summary>
    /// Name of the module of related commands
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// General description of the module
    /// </summary>
    public string Description { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleAttribute"/> class with the specified name and description
    /// </summary>
    /// <param name="name">The module name</param>
    /// <param name="description">The module description</param>
    public ModuleAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
