using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MailKit;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using AvcolForms.Core.Options;

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
    public EmailSender(IOptions<EmailOptions> options, ILogger<IEmailSender> logger)
    {
        Options = options.Value;
        Logger = logger;
    }

    /// <summary>
    /// Settings used for the email sender
    /// </summary>
    public EmailOptions Options { get; }

    /// <inheritdoc></inheritdoc>
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Logger.LogInformation("Executing sending email operation to {email}", email);
        await ExecuteAsync(subject, htmlMessage, email);
    }


    /// <summary>
    /// Operation of sending a user an email
    /// </summary>
    /// <param name="subject">The subject of the email</param>
    /// <param name="message">The message of the email</param>
    /// <param name="toEmail"></param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    public async Task ExecuteAsync(string subject, string message, string toEmail)
    {
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(Options.FromEmail),
            Subject = subject,
            Body = new TextPart(TextFormat.Html) { Text = message }
        };

        email.Sender.Name = Options.SenderName;

        email.From.Add(email.Sender);

        email.To.Add(MailboxAddress.Parse(toEmail));

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(Options.SmtpServer, Options.Port, true).ConfigureAwait(false);

        await smtpClient.AuthenticateAsync(Options.UserName, Options.Password).ConfigureAwait(false);

        var response = await smtpClient.SendAsync(email).ConfigureAwait(false);

        Logger.LogInformation("{response}", response);
    }
}
