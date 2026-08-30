using Microsoft.EntityFrameworkCore;
using MVCMiniApp.Data;
using MVCMiniAPpp.Helpers;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public sealed class BlogService(AppDbContext db) : IBlogService
{
    public async Task<PaginatedList<Blog>> GetPublishedAsync(
        string? search = null,
        int? categoryId = null,
        int pageIndex = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = db.Blogs.AsNoTracking()
            .Include(blog => blog.Category)
            .Where(blog => blog.IsPublished && blog.PublishedAt != null);

        if (categoryId is > 0)
            query = query.Where(blog => blog.CategoryId == categoryId.Value);
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(blog => blog.Title.Contains(search) ||
                                        (blog.Summary != null && blog.Summary.Contains(search)));
        }

        return await PaginatedList<Blog>.CreateAsync(
            query.OrderByDescending(blog => blog.PublishedAt), pageIndex, pageSize, cancellationToken);
    }

    public Task<Blog?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        return PublishedQuery().SingleOrDefaultAsync(blog => blog.Id == id, cancellationToken);
    }

    public Task<Blog?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new ArgumentException("A slug is required.", nameof(slug));
        return PublishedQuery().SingleOrDefaultAsync(blog => blog.Slug == slug.Trim(), cancellationToken);
    }

    private IQueryable<Blog> PublishedQuery() => db.Blogs.AsNoTracking()
        .Include(blog => blog.Category)
        .Where(blog => blog.IsPublished && blog.PublishedAt != null);
}
