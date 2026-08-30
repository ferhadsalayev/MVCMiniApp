using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public sealed class EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger) : IEmailService
{
    private readonly EmailSettings settings = options.Value;

    public async Task SendAsync(string recipient, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(recipient)) throw new ArgumentException("A recipient is required.", nameof(recipient));
        if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentException("A subject is required.", nameof(subject));
        if (body is null) throw new ArgumentNullException(nameof(body));
        ValidateSettings();

        using var message = new MailMessage
        {
            From = new MailAddress(settings.SenderEmail, settings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };
        message.To.Add(new MailAddress(recipient));

        using var client = new SmtpClient(settings.Host, settings.Port)
        {
            EnableSsl = settings.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };
        if (!string.IsNullOrWhiteSpace(settings.Username))
            client.Credentials = new NetworkCredential(settings.Username, settings.Password);

        try
        {
            await client.SendMailAsync(message, cancellationToken);
        }
        catch (SmtpException exception)
        {
            logger.LogError(exception, "Unable to send email to {Recipient}.", recipient);
            throw new InvalidOperationException("The email could not be sent.", exception);
        }
    }

    private void ValidateSettings()
    {
        if (string.IsNullOrWhiteSpace(settings.Host) || string.IsNullOrWhiteSpace(settings.SenderEmail))
            throw new InvalidOperationException("EmailSettings:Host and EmailSettings:SenderEmail must be configured.");
        if (!string.IsNullOrWhiteSpace(settings.Username) && string.IsNullOrWhiteSpace(settings.Password))
            throw new InvalidOperationException("EmailSettings:Password must be configured when Username is set.");
    }
}
