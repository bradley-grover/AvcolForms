namespace AvcolForms.Core;

/// <summary>
/// Constants used by the core library for things like lengths
/// </summary>
public static class Constants
{
    /// <summary>
    /// The maximum amount of characters allowed in a email address
    /// </summary>
    public const int MaxEmailLength = 256;

    /// <summary>
    /// The error message for when the email is too long
    /// </summary>
    public const string MaxEmailLengthError = $"The maximum allowed length for an email is 256";

    /// <summary>
    /// Static class to hold data of all assemblies in the project
    /// </summary>
    public static class Assemblies
    {
        /// <summary>
        /// The executable project of the application
        /// </summary>
        public const string Executable = "AvcolForms.Web";

        /// <summary>
        /// The core library of the project
        /// </summary>
        public const string Library = "AvcolForms.Core";

        /// <summary>
        /// The data access layer of the project
        /// </summary>
        public const string DataAccessLayer = "AvcolForms.Core.Data";

        /// <summary>
        /// The tests executable of the application
        /// </summary>
        public const string TestsExecutable = "AvcolForms.Tests";

        /// <summary>
        /// Debug helpers for the main executable
        /// </summary>
        public const string DebugExecutable = "AvcolForms.Config.App";

        /// <summary>
        /// The exectuable may use these for certain parts of the application
        /// </summary>
        public const string ExecutableComponents = "AvcolForms.Core.Components";

        /// <summary>
        /// The assemblies that extends of of <see cref="DataAccessLayer"/>  
        /// </summary>
        public static IReadOnlyCollection<string> DataAccessLayerProviders => Array.AsReadOnly(new string[]
        {
            "AvcolForms.Core.Data.Postgres",
            "AvcolForms.Core.Data.Sqlite",
            "AvcolForms.Core.Data.SqlServer"
        });
    }
}
