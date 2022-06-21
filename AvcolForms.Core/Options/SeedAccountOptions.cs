using System.ComponentModel.DataAnnotations;

namespace AvcolForms.Core.Options;

#nullable disable

/// <summary>
/// Used for seeding accounts
/// </summary>
public class SeedAccountOptions
{
    /// <summary>
    /// Accounts to add to the application
    /// </summary>
    [Required]
    public AccountModel[] Accounts { get; set; }
}
