/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Core.Accounts;

#nullable disable

/// <summary>
/// Model for registering a seeded account
/// </summary>
public class AccountModel
{
    /// <summary>
    /// Email address for the account
    /// </summary>
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// Password for the account
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

#nullable restore

    /// <summary>
    /// The role assigned to the account on initialization
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Whether the account should bypass email authentication
    /// </summary>
    public bool? BypassEmail { get; set; }
}
