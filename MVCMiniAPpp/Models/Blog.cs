using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class Blog : BaseEntity
{
    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(220)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Summary { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [StringLength(150)]
    public string? AuthorName { get; set; }

    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}
