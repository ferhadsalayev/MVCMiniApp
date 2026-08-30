using MVCMiniAPpp.Helpers;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface IProductService
{
    Task<PaginatedList<Product>> GetAsync(string? search = null, int? categoryId = null, int pageIndex = 1, int pageSize = 12, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetFeaturedAsync(int count = 8, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
}
