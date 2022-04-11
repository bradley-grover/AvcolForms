/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AvcolForms.Core.Data;

/// <summary>
/// The data context for out application
/// </summary>
public class ApplicationDbContext : IdentityDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options
    /// </summary>
    /// <param name="options">The options to be passed into the context</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
