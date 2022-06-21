/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using System.Reflection;

namespace AvcolForms.Web;

/// <summary>
/// Program class for the application, in this class the program starts from <see cref="Main(string[])"/> method
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point for the application
    /// </summary>
    /// <param name="args">Arguments supplied for the builder passed in</param>
    public static void Main(string[] args)
    {
        Console.Title = Assembly.GetCallingAssembly().GetName().FullName;

        CreateHostBuilder(args).Build().Run();
    }
    /// <summary>
    /// Creates the <see cref="IHostBuilder"/> to be used as <see cref="IHost"/> to run our application
    /// </summary>
    /// <param name="args">Arguments to be supplied to the builder</param>
    /// <returns>A <see cref="IHostBuilder"/></returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}
