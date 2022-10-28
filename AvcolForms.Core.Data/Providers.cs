using System.Reflection;

namespace AvcolForms.Core.Data;

/// <summary>
/// The database providers
/// </summary>
public static class Providers
{
    /// <summary>
    /// Initializes the static Providers class
    /// </summary>
    static Providers()
    {
        CaseInsensitiveProviders = DbProviders.Select(x => x.ToLower()).ToArray();
    }
    /// <summary>
    /// The JSON property name that specifies which DbProvider is being used
    /// </summary>
    public const string DbProviderKey = "Db-Provider";

    /// <summary>
    /// The <see cref="Assembly"/> that the Postgres EF migrations are stored in
    /// </summary>
    public const string PostgresAssembly = "AvcolForms.Core.Data.Postgres";

    /// <summary>
    /// The <see cref="Assembly"/> that the SqlServer EF migrations are stored in
    /// </summary>
    public const string SqlServerAssembly = "AvcolForms.Core.Data.SqlServer";

    /// <summary>
    /// The <see cref="Assembly"/> that the SQLite EF migrations are stored in
    /// </summary>
    public const string SqliteAssembly = "AvcolForms.Core.Data.Sqlite";

    /// <summary>
    /// Sqlite provider name in json key
    /// </summary>
    public const string SqliteProvider = "Sqlite";

    /// <summary>
    /// SqlServer provider name in json key
    /// </summary>
    public const string SqlServerProvider = "SqlServer";

    /// <summary>
    /// Postgres provider name in json key
    /// </summary>
    public const string PostgresProvider = "Postgres";

    /// <summary>
    /// If the provider is a supported provider
    /// </summary>
    /// <param name="provider">The provider</param>
    /// <returns><see langword="true"/> if the provider is supported</returns>
    public static bool IsSupportedProvider(string provider)
    {
        ArgumentNullException.ThrowIfNull(provider, nameof(provider));

        for (int i = 0; i < DbProviders.Length; i++)
        {
            if (provider.ToLower() == DbProviders[i].ToLower())
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets the <see cref="Provider"/> from the string
    /// </summary>
    /// <param name="provider"></param>
    /// <returns>A <see cref="Provider"/>, could be <see langword="null"/></returns>
    public static Provider? GetProvider(string provider)
    {
        return provider.ToLower() switch
        {
            "postgres" => (Provider?)Provider.Postgres,
            "sqlite" => (Provider?)Provider.Sqlite,
            "sqlserver" => (Provider?)Provider.SqlServer,
            _ => null,
        };
    }

    /// <summary>
    /// The database providers that are supported
    /// </summary>
    public static string[] DbProviders { get; } = new string[]
    {
        SqliteProvider,
        SqlServerProvider,
        PostgresProvider
    };
    
    /// <summary>
    /// <see cref="DbProviders"/> but case insensitive
    /// </summary>
    public static string[] CaseInsensitiveProviders { get; }
}
