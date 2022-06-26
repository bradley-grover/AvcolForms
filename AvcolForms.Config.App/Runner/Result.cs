namespace AvcolForms.Config.App.Runner;

/// <summary>
/// Result type of executing the command
/// </summary>
public enum Result
{
    /// <summary>
    /// The runner found no command
    /// </summary>
    NotFound,
    /// <summary>
    /// The runner successfully ran the command
    /// </summary>
    Success,
}
