using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.Services;

namespace MVCMiniAPpp.ViewComponents.BlogDetail;

public sealed class BlogDetailViewComponent(IBlogService blogs) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string slug, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new ArgumentException("A blog slug is required.", nameof(slug));
        return View(await blogs.GetBySlugAsync(slug, cancellationToken));
    }
}
