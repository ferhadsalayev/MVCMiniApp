using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class ContactMessage : BaseEntity
{
    [Required, StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required, StringLength(4000)]
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }
}
