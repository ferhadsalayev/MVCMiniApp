namespace MVCMiniAPpp.Models;

public class Basket : BaseEntity
{
    public int UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
}
