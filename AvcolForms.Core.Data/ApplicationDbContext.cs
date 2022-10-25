/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using AvcolForms.Core.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AvcolForms.Core.Data;

#nullable disable

/// <summary>
/// The data context for out application
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options
    /// </summary>
    /// <param name="options">The options to be passed into the context</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// The table 'Forms' of the database
    /// </summary>
    public DbSet<Form> Forms { get; set; }
}
