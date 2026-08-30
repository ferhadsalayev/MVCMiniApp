namespace MVCMiniAPpp.Models;

public class Wishlist : BaseEntity
{
    public int UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
}
