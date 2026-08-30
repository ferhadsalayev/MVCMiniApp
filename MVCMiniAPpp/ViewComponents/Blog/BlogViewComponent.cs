using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.Services;
using MVCMiniAPpp.ViewModels;

namespace MVCMiniAPpp.ViewComponents.Blog;

public sealed class BlogViewComponent(IBlogService blogs) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int pageIndex = 1, int pageSize = 3, string? search = null, int? categoryId = null, CancellationToken cancellationToken = default) =>
        View(new BlogComponentViewModel { Posts = await blogs.GetPublishedAsync(search, categoryId, pageIndex, pageSize, cancellationToken) });
}
