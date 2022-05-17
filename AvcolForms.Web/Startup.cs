/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using AvcolForms.Web.ServiceConfigures;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace AvcolForms.Web;

/// <summary>
/// Startup class to register application services and the HTTP Request Pipeline
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class
    /// </summary>
    /// <param name="configuration">The configuration used for the application</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// The configuration for our web application
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Configures and registers services for the web application
    /// </summary>
    /// <param name="services">The service collection to add services for dependency injection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMudServices();
        services.AddRazorPages();
        services.AddServerSideBlazor();

        services.AddDb(Configuration);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
    }

    /// <summary>
    /// Configures the HTTP request pipeline
    /// </summary>
    /// <param name="app">The web host environment automatically passed in</param>
    /// <param name="env">The application builder automatically passed in</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
