namespace AvcolForms.Core.Data;

/// <summary>
/// A database provider that is configured
/// </summary>
public enum Provider
{
    /// <summary>
    /// MSql database server provider
    /// </summary>
    SqlServer,
    /// <summary>
    /// Sqlite, small compact db provider
    /// </summary>
    Sqlite,
    /// <summary>
    /// Network based postgres provider
    /// </summary>
    Postgres
}
