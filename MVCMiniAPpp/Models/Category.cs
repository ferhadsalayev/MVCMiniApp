using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class Category : BaseEntity
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
