using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class Testimonial : BaseEntity
{
    [Required, StringLength(120)]
    public string AuthorName { get; set; } = string.Empty;

    [StringLength(120)]
    public string? Position { get; set; }

    [Required, StringLength(2000)]
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
