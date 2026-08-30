using System.ComponentModel.DataAnnotations;
using MVCMiniAPpp.Helpers;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.ViewModels;

public sealed class ProductListViewModel
{
    public PaginatedList<Product> Products { get; init; } = null!;
    public string? Search { get; init; }
    public int? CategoryId { get; init; }
}

public sealed class BasketViewModel
{
    public IReadOnlyList<BasketItem> Items { get; init; } = [];
    public decimal Total { get; init; }
}

public sealed class WishlistViewModel
{
    public IReadOnlyList<WishlistItem> Items { get; init; } = [];
}

public sealed class RegisterViewModel
{
    [Required, StringLength(100)] public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(100)] public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, DataType(DataType.Password), MinLength(8)] public string Password { get; set; } = string.Empty;
    [Required, DataType(DataType.Password), Compare(nameof(Password))] public string ConfirmPassword { get; set; } = string.Empty;
}

public sealed class LoginViewModel
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}

public sealed class ContactViewModel
{
    [Required, StringLength(150)] public string Name { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, StringLength(200)] public string Subject { get; set; } = string.Empty;
    [Required, StringLength(4000)] public string Message { get; set; } = string.Empty;
}
