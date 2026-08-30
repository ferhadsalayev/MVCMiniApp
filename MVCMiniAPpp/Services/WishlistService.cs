using Microsoft.EntityFrameworkCore;
using MVCMiniApp.Data;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public sealed class WishlistService(AppDbContext db) : IWishlistService
{
    public async Task<Wishlist> GetAsync(int userId, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);
        var wishlist = await db.Wishlists
            .Include(item => item.Items)
                .ThenInclude(item => item.Product)
            .SingleOrDefaultAsync(item => item.UserId == userId, cancellationToken);
        if (wishlist is not null) return wishlist;

        wishlist = new Wishlist { UserId = userId };
        db.Wishlists.Add(wishlist);
        await db.SaveChangesAsync(cancellationToken);
        return wishlist;
    }

    public async Task<bool> AddAsync(int userId, int productId, CancellationToken cancellationToken = default)
    {
        ValidateIds(userId, productId);
        var productExists = await db.Products.AnyAsync(product => product.Id == productId && product.IsAvailable, cancellationToken);
        if (!productExists) throw new KeyNotFoundException("The product was not found or is unavailable.");
        if (await ContainsAsync(userId, productId, cancellationToken)) return false;

        var wishlist = await db.Wishlists.SingleOrDefaultAsync(item => item.UserId == userId, cancellationToken);
        if (wishlist is null)
        {
            wishlist = new Wishlist { UserId = userId };
            db.Wishlists.Add(wishlist);
        }
        db.WishlistItems.Add(new WishlistItem { Wishlist = wishlist, ProductId = productId });
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RemoveAsync(int userId, int productId, CancellationToken cancellationToken = default)
    {
        ValidateIds(userId, productId);
        var item = await db.WishlistItems.SingleOrDefaultAsync(
            value => value.Wishlist.UserId == userId && value.ProductId == productId, cancellationToken);
        if (item is null) return false;
        db.WishlistItems.Remove(item);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<bool> ContainsAsync(int userId, int productId, CancellationToken cancellationToken = default)
    {
        ValidateIds(userId, productId);
        return db.WishlistItems.AnyAsync(item => item.Wishlist.UserId == userId && item.ProductId == productId, cancellationToken);
    }

    private static void ValidateIds(int userId, int productId)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
    }
    private static void ValidateUserId(int userId)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
    }
}
