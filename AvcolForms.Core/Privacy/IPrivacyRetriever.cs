namespace AvcolForms.Core.Privacy;

/// <summary>
/// Retrieves the privacy policy to display to the user
/// </summary>
public interface IPrivacyRetriever
{
    /// <summary>
    /// Retrieves the privacy policy for string format
    /// </summary>
    /// <returns>A <see langword="string"/> of the policy</returns>
    ValueTask<string> RetrieveAsync();
}
