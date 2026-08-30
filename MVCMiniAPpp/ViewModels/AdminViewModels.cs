using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVCMiniAPpp.ViewModels;

public sealed class ProductFormViewModel
{
    public int Id { get; set; }
    [Required, StringLength(200)] public string Name { get; set; } = string.Empty;
    [Required, StringLength(120)] public string Slug { get; set; } = string.Empty;
    [Required, StringLength(64)] public string SKU { get; set; } = string.Empty;
    [StringLength(2000)] public string? Description { get; set; }
    [Range(0, 999999999)] public decimal Price { get; set; }
    [Range(0, 999999999)] public decimal? DiscountPrice { get; set; }
    [Range(0, int.MaxValue)] public int StockQuantity { get; set; }
    [Required] public int CategoryId { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsAvailable { get; set; } = true;
    public IFormFile? Image { get; set; }
}

public sealed class CategoryFormViewModel
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
    [Required, StringLength(120)] public string Slug { get; set; } = string.Empty;
    [StringLength(500)] public string? Description { get; set; }
    public IFormFile? Image { get; set; }
}

public sealed class BlogFormViewModel
{
    public int Id { get; set; }
    [Required, StringLength(200)] public string Title { get; set; } = string.Empty;
    [Required, StringLength(220)] public string Slug { get; set; } = string.Empty;
    [Required] public string Content { get; set; } = string.Empty;
    [StringLength(500)] public string? Summary { get; set; }
    public int? CategoryId { get; set; }
    public string? AuthorName { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; }
    public IFormFile? Image { get; set; }
}

public sealed class SettingFormViewModel
{
    [Required, StringLength(100)] public string Key { get; set; } = string.Empty;
    [Required, StringLength(4000)] public string Value { get; set; } = string.Empty;
}

public sealed class SliderFormViewModel
{
    public int Id { get; set; }
    [Required, StringLength(200)] public string Title { get; set; } = string.Empty;
    [StringLength(500)] public string? Subtitle { get; set; }
    [StringLength(500)] public string? LinkUrl { get; set; }
    [Range(0, int.MaxValue)] public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public IFormFile? Image { get; set; }
}

public sealed class TeamFormViewModel
{
    public int Id { get; set; }
    [Required, StringLength(120)] public string FullName { get; set; } = string.Empty;
    [Required, StringLength(120)] public string Position { get; set; } = string.Empty;
    [StringLength(500)] public string? Biography { get; set; }
    [Url, StringLength(300)] public string? LinkedInUrl { get; set; }
    [Url, StringLength(300)] public string? TwitterUrl { get; set; }
    [Range(0, int.MaxValue)] public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public IFormFile? Image { get; set; }
}

public sealed class TestimonialFormViewModel
{
    public int Id { get; set; }
    [Required, StringLength(120)] public string AuthorName { get; set; } = string.Empty;
    [StringLength(120)] public string? Position { get; set; }
    [Required, StringLength(2000)] public string Content { get; set; } = string.Empty;
    [Range(1, 5)] public int Rating { get; set; } = 5;
    [Range(0, int.MaxValue)] public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public IFormFile? Image { get; set; }
}
