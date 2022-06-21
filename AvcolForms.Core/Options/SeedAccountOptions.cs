using System.ComponentModel.DataAnnotations;
using AvcolForms.Core.Accounts;

namespace AvcolForms.Core.Options;

/// <summary>
/// Used for seeding accounts
/// </summary>
public class SeedAccountOptions
{
    /// <summary>
    /// Accounts to add to the application
    /// </summary>
    public AccountModel[]? Accounts { get; set; }
}
