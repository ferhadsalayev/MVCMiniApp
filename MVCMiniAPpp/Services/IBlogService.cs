using MVCMiniAPpp.Helpers;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface IBlogService
{
    Task<PaginatedList<Blog>> GetPublishedAsync(string? search = null, int? categoryId = null, int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<Blog?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Blog?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
}
