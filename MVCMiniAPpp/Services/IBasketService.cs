using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface IBasketService
{
    Task<Basket> GetAsync(int userId, CancellationToken cancellationToken = default);
    Task<BasketItem> AddItemAsync(int userId, int productId, int quantity = 1, CancellationToken cancellationToken = default);
    Task<bool> RemoveItemAsync(int userId, int productId, CancellationToken cancellationToken = default);
    Task<bool> UpdateQuantityAsync(int userId, int productId, int quantity, CancellationToken cancellationToken = default);
    Task ClearAsync(int userId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalAsync(int userId, CancellationToken cancellationToken = default);
}
