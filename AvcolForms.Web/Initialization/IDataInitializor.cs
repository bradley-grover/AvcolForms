namespace AvcolForms.Web.Initialization;

/// <summary>
/// Initializes the database with data
/// </summary>
public interface IDataInitializor
{
    /// <summary>
    /// Method to call to initialize the database
    /// </summary>
    void Initialize();
}
