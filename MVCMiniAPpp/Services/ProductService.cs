using Microsoft.EntityFrameworkCore;
using MVCMiniAPpp.Helpers;
using MVCMiniAPpp.Models;
using MVCMiniApp.Data;

namespace MVCMiniAPpp.Services;

public sealed class ProductService(AppDbContext db) : IProductService
{
    public async Task<PaginatedList<Product>> GetAsync(string? search = null, int? categoryId = null, int pageIndex = 1, int pageSize = 12, CancellationToken cancellationToken = default)
    {
        var query = db.Products.AsNoTracking().Include(product => product.Category).Where(product => product.IsAvailable);
        if (categoryId is > 0) query = query.Where(product => product.CategoryId == categoryId.Value);
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(product => product.Name.Contains(search) || product.SKU.Contains(search));
        }
        return await PaginatedList<Product>.CreateAsync(query.OrderBy(product => product.Name), pageIndex, pageSize, cancellationToken);
    }

    public Task<IReadOnlyList<Product>> GetFeaturedAsync(int count = 8, CancellationToken cancellationToken = default)
    {
        count = Math.Clamp(count, 1, 50);
        return db.Products.AsNoTracking().Include(product => product.Category)
            .Where(product => product.IsAvailable && product.IsFeatured)
            .OrderBy(product => product.Name).Take(count).ToListAsync(cancellationToken)
            .ContinueWith(task => (IReadOnlyList<Product>)task.Result, cancellationToken);
    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        return db.Products.AsNoTracking().Include(product => product.Category)
            .SingleOrDefaultAsync(product => product.Id == id && product.IsAvailable, cancellationToken);
    }

    public Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new ArgumentException("A slug is required.", nameof(slug));
        return db.Products.AsNoTracking().Include(product => product.Category)
            .SingleOrDefaultAsync(product => product.Slug == slug.Trim() && product.IsAvailable, cancellationToken);
    }
}
