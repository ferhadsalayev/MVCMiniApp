using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class Setting : BaseEntity
{
    [Required, StringLength(100)]
    public string Key { get; set; } = string.Empty;

    [Required, StringLength(4000)]
    public string Value { get; set; } = string.Empty;
}
