using System.Diagnostics.CodeAnalysis;

namespace AvcolForms.Web.Initialization;

/// <summary>
/// Initializes the database with data
/// </summary>
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public interface IDataInitializor
{
    /// <summary>
    /// Method to call to initialize the database
    /// </summary>
    void Initialize();
}
