using Microsoft.EntityFrameworkCore;
using MVCMiniApp.Data;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public sealed class CategoryService(AppDbContext db) : ICategoryService
{
    public async Task<IReadOnlyList<Category>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await db.Categories.AsNoTracking()
            .Where(category => category.Products.Any(product => product.IsAvailable))
            .OrderBy(category => category.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        return db.Categories.AsNoTracking()
            .Include(category => category.Products.Where(product => product.IsAvailable))
            .SingleOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new ArgumentException("A slug is required.", nameof(slug));
        slug = slug.Trim();
        return db.Categories.AsNoTracking()
            .Include(category => category.Products.Where(product => product.IsAvailable))
            .SingleOrDefaultAsync(category => category.Slug == slug, cancellationToken);
    }
}
