namespace MVCMiniAPpp.Services;

public interface IEmailService
{
    Task SendAsync(string recipient, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default);
}
