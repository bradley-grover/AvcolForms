using System.ComponentModel.DataAnnotations;
using AvcolForms.Core.Options;
using Microsoft.Extensions.Options;

namespace AvcolForms.Web.ServiceConfigures;

internal static class OptionExtensions
{
    /// <summary>
    /// Add options to the container
    /// </summary>
    /// <returns>The same <see cref="IServiceCollection"/> used for chaining</returns>
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();


        services.ConfigureAndValidate<PrivacyOptions>(nameof(PrivacyOptions), configuration);
        services.ConfigureAndValidate<EmailOptions>(nameof(EmailOptions), configuration);
        services.ConfigureAndValidate<WebsiteOptions>(nameof(WebsiteOptions), configuration);
        services.ConfigureAndValidate<SeedAccountOptions>(nameof(SeedAccountOptions), configuration);

        return services;
    }

    private static void ValidateByDataAnnotation<T>(T instance, string sectionName) where T : notnull
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(instance);
        var valid = Validator.TryValidateObject(instance, context, validationResults);
        if (valid)
        {
            return;
        }

        ReadOnlySpan<char> msg = string.Join("\n", validationResults.Select(r => r.ErrorMessage));

        throw new Exception($"Invalid configuration of section '{sectionName}':\n{msg}");
    }

    public static OptionsBuilder<TOptions> ValidateByDataAnnotation<TOptions>(
        this OptionsBuilder<TOptions> builder,
        string sectionName)
        where TOptions : class
    {
        return builder.PostConfigure(x => ValidateByDataAnnotation(x, sectionName));
    }

    public static IServiceCollection ConfigureAndValidate<TOptions>(
        this IServiceCollection services,
        string sectionName,
        IConfiguration configuration)
        where TOptions : class
    {
        var section = configuration.GetSection(sectionName);

        services
            .AddOptions<TOptions>()
            .Bind(section)
            .ValidateByDataAnnotation(sectionName);

        return services;
    }
}
