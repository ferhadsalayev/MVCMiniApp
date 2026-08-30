using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class Team : BaseEntity
{
    [Required, StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Position { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Biography { get; set; }

    [StringLength(300)]
    public string? LinkedInUrl { get; set; }

    [StringLength(300)]
    public string? TwitterUrl { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
