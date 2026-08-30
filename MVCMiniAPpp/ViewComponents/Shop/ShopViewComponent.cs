using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.Services;
using MVCMiniAPpp.ViewModels;

namespace MVCMiniAPpp.ViewComponents.Shop;

public sealed class ShopViewComponent(IProductService products) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string? search = null, int? categoryId = null, int pageIndex = 1, int pageSize = 12, CancellationToken cancellationToken = default) =>
        View(new ProductListViewModel
        {
            Products = await products.GetAsync(search, categoryId, pageIndex, pageSize, cancellationToken),
            Search = search,
            CategoryId = categoryId
        });
}
