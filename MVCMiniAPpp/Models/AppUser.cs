using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class AppUser : BaseEntity
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(30)]
    public string? PhoneNumber { get; set; }

    [Required, StringLength(30)]
    public string Role { get; set; } = "User";

    public Basket? Basket { get; set; }
    public Wishlist? Wishlist { get; set; }
}
