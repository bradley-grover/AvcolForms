/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using AvcolForms.Web.ServiceConfigures;

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
        services.AddRazorPages();
        services.AddServerSideBlazor();

        services.AddDb(Configuration);
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
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
