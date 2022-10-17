/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

namespace AvcolForms.Core.Options;

#nullable disable

/// <summary>
/// Top level user created because env vars can't be arrays
/// </summary>
public sealed class RootUserOptions
{
    /// <summary>
    /// Email address for the account
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(Constants.MaxEmailLength)]
    public string Email { get; set; }

    /// <summary>
    /// Password to authenticate the sign in
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
