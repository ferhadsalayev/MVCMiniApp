using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface IWishlistService
{
    Task<Wishlist> GetAsync(int userId, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(int userId, int productId, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(int userId, int productId, CancellationToken cancellationToken = default);
    Task<bool> ContainsAsync(int userId, int productId, CancellationToken cancellationToken = default);
}
