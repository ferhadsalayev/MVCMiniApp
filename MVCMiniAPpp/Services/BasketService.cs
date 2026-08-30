using Microsoft.EntityFrameworkCore;
using MVCMiniApp.Data;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public sealed class BasketService(AppDbContext db) : IBasketService
{
    public async Task<Basket> GetAsync(int userId, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);
        var basket = await db.Baskets
            .Include(item => item.Items)
                .ThenInclude(item => item.Product)
            .SingleOrDefaultAsync(item => item.UserId == userId, cancellationToken);

        if (basket is not null)
            return basket;

        basket = new Basket { UserId = userId };
        db.Baskets.Add(basket);
        await db.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<BasketItem> AddItemAsync(int userId, int productId, int quantity = 1, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);
        if (productId <= 0 || quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(productId), "Product and quantity must be positive.");

        var product = await db.Products.SingleOrDefaultAsync(item => item.Id == productId, cancellationToken)
            ?? throw new KeyNotFoundException("The product was not found.");
        if (!product.IsAvailable || product.StockQuantity < quantity)
            throw new InvalidOperationException("The product is unavailable or there is not enough stock.");

        var basket = await db.Baskets.SingleOrDefaultAsync(item => item.UserId == userId, cancellationToken);
        if (basket is null)
        {
            basket = new Basket { UserId = userId };
            db.Baskets.Add(basket);
        }

        var basketItem = await db.BasketItems.SingleOrDefaultAsync(
            item => item.Basket.UserId == userId && item.ProductId == productId, cancellationToken);
        if (basketItem is null)
        {
            basketItem = new BasketItem { Basket = basket, Product = product, Quantity = quantity, UnitPrice = GetPrice(product) };
            db.BasketItems.Add(basketItem);
        }
        else
        {
            if (basketItem.Quantity + quantity > product.StockQuantity)
                throw new InvalidOperationException("There is not enough stock for the requested quantity.");
            basketItem.Quantity += quantity;
            basketItem.UnitPrice = GetPrice(product);
        }

        await db.SaveChangesAsync(cancellationToken);
        return basketItem;
    }

    public async Task<bool> RemoveItemAsync(int userId, int productId, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);
        var item = await db.BasketItems.SingleOrDefaultAsync(
            value => value.Basket.UserId == userId && value.ProductId == productId, cancellationToken);
        if (item is null) return false;
        db.BasketItems.Remove(item);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateQuantityAsync(int userId, int productId, int quantity, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);
        if (quantity <= 0) return await RemoveItemAsync(userId, productId, cancellationToken);

        var item = await db.BasketItems.Include(value => value.Product).SingleOrDefaultAsync(
            value => value.Basket.UserId == userId && value.ProductId == productId, cancellationToken);
        if (item is null) return false;
        if (!item.Product.IsAvailable || quantity > item.Product.StockQuantity)
            throw new InvalidOperationException("The requested quantity is not available.");

        item.Quantity = quantity;
        item.UnitPrice = GetPrice(item.Product);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task ClearAsync(int userId, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);
        var items = await db.BasketItems.Where(item => item.Basket.UserId == userId).ToListAsync(cancellationToken);
        if (items.Count == 0) return;
        db.BasketItems.RemoveRange(items);
        await db.SaveChangesAsync(cancellationToken);
    }

    public Task<decimal> GetTotalAsync(int userId, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);
        return db.BasketItems.Where(item => item.Basket.UserId == userId)
            .Select(item => (decimal?)item.UnitPrice * item.Quantity)
            .SumAsync(cancellationToken)
            .ContinueWith(task => task.Result ?? 0m, cancellationToken);
    }

    private static decimal GetPrice(Product product) => product.DiscountPrice is > 0 && product.DiscountPrice < product.Price
        ? product.DiscountPrice.Value : product.Price;

    private static void ValidateUserId(int userId)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
    }
}
