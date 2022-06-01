namespace AvcolForms.Core.Email;

/// <summary>
/// Class to hold user secrets of email key
/// </summary>
public class AuthMessageSenderOptions
{
    /// <summary>
    /// SendGrid api key for authorization
    /// </summary>
    public string? SendGridKey { get; set; }
}
