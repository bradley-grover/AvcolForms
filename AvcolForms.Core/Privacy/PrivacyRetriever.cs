using AvcolForms.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AvcolForms.Core.Privacy;

/// <summary>
/// Class to retrieve the privacy policy to display to the user
/// </summary>
public class PrivacyRetriever : IPrivacyRetriever
{
    private IOptions<PrivacyOptions> Options { get; }
    private ILogger<IPrivacyRetriever> Logger { get; }

    private string? _privacyContent = null;
    private bool retrieved;
    private readonly object _lock = new();

    /// <summary>
    /// 
    /// </summary>
    public PrivacyRetriever(IOptions<PrivacyOptions> options, ILogger<IPrivacyRetriever> logger)
    {
        ArgumentNullException.ThrowIfNull(options.Value, nameof(options));

        Options = options;
        Logger = logger;
    }

    /// <inheritdoc></inheritdoc>
    public async ValueTask<string> RetrieveAsync()
    {
        if (!retrieved)
        {
            try
            {
                using FileStream stream = File.OpenRead(Options.Value.FileName);

                using var streamReader = new StreamReader(stream);

                _privacyContent = await streamReader.ReadToEndAsync();
            }
            catch (Exception exception)
            {
                Logger.LogError("{exception}", exception);
            }
            finally
            {
                Logger.LogInformation("Privacy Retriever has ran setup");
                retrieved = true;
            }
            
        }
        
        lock (_lock)
        {
            return _privacyContent ?? "No privacy policy has been configured for this application";
        }
    }
}
