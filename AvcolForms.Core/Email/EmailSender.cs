using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AvcolForms.Core.Email;

/// <summary>
/// Email sender used for verifying email authentication
/// </summary>
public class EmailSender : IEmailSender
{
    private ILogger<IEmailSender> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSender"/> class
    /// </summary>
    /// <param name="options">Options to configure authentication for the email client</param>
    /// <param name="logger">Logger to log events</param>
    public EmailSender(IOptions<AuthMessageSenderOptions> options, ILogger<IEmailSender> logger)
    {
        Options = options.Value;
        Logger = logger;
    }

    /// <summary>
    /// Authorizes clients
    /// </summary>
    public AuthMessageSenderOptions Options { get; }

    /// <inheritdoc></inheritdoc>
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new InvalidOperationException($"{nameof(Options.SendGridKey)} is not configured");
        }

        await ExecuteAsync(Options.SendGridKey, subject, htmlMessage, email);
    }


    /// <summary>
    /// Operation of sending a user an email
    /// </summary>
    /// <param name="apiKey">Api key for the email client</param>
    /// <param name="subject">The subject of the email</param>
    /// <param name="message">The message of the email</param>
    /// <param name="toEmail"></param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    public async Task ExecuteAsync(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);

        var sendGridMessage = new SendGridMessage()
        {
            From = new EmailAddress("joe@contoso.com", "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };

        sendGridMessage.AddTo(toEmail);

        sendGridMessage.SetClickTracking(false, false); // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html for relevant implications of this setting being applied on the email

        var response = await client.SendEmailAsync(sendGridMessage).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            Logger.LogInformation("Email to {toEmail} queued successfully", toEmail);
        }
        else
        {
            Logger.LogInformation("Email to {toEmail} has failed", toEmail);
        }
    }
}
