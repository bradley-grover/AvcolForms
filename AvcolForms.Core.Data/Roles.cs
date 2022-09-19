namespace AvcolForms.Core.Data;

/// <summary>
/// Application roles that are available
/// </summary>
public static class Roles
{
    /// <summary>
    /// Admin role (constant)
    /// </summary>
    public const string Admin = nameof(Admin);

    /// <summary>
    /// Form admin role (constant)
    /// </summary>
    public const string FormAdmin = nameof(FormAdmin);

    /// <summary>
    /// Form creator role (constant)
    /// </summary>
    public const string FormCreation = nameof(FormCreation);

    /// <summary>
    /// Represents every role in the database
    /// </summary>
    public static ReadOnlySpan<string> Every => _everyStore;


    internal static string[] _everyStore = new string[]
    {
        Admin,
        FormAdmin,
        FormCreation
    };
}
