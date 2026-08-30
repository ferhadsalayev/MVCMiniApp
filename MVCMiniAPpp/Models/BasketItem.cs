using System.ComponentModel.DataAnnotations;

namespace MVCMiniAPpp.Models;

public class BasketItem : BaseEntity
{
    public int BasketId { get; set; }
    public Basket Basket { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;

    [Range(0, 999999999)]
    public decimal UnitPrice { get; set; }
}
