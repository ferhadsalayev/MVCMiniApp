using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class EmailSettings
{
    [Required]
    public string Host { get; set; } = string.Empty;

    [Range(1, 65535)]
    public int Port { get; set; } = 587;

    public bool EnableSsl { get; set; } = true;

    [Required, EmailAddress]
    public string SenderEmail { get; set; } = string.Empty;

    [Required]
    public string SenderName { get; set; } = string.Empty;

    public string? Username { get; set; }
    public string? Password { get; set; }
}
