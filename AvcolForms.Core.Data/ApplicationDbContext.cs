/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using System.Text.Json;
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
    /// Configures the model builder
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    /// <summary>
    /// The table 'Forms' of the database
    /// </summary>
    public DbSet<Form> Forms { get; set; }

    /// <summary>
    /// The table 'Responses' of the database. This table contains the responses to items in the <see cref="Forms"/> table linked by key
    /// </summary>
    public DbSet<FormResponse> Responses { get; set; }

    /// <summary>
    /// Content inside of a response
    /// </summary>
    public DbSet<FormContentResponse> ContentResponses { get; set; }

    /// <summary>
    /// Content inside forms
    /// </summary>
    public DbSet<FormContent> Contents { get; set; }
}
