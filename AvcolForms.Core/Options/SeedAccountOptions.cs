/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using AvcolForms.Core.Accounts;

namespace AvcolForms.Core.Options;

/// <summary>
/// Used for seeding accounts
/// </summary>
public sealed class SeedAccountOptions
{
    /// <summary>
    /// Accounts to add to the application
    /// </summary>
    public AccountModel[]? Accounts { get; set; }
}
