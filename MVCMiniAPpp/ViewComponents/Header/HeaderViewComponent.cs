using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.Services;
using MVCMiniAPpp.ViewModels;

namespace MVCMiniAPpp.ViewComponents.Header;

public sealed class HeaderViewComponent(ICategoryService categories, IBasketService basket) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        var categoryTask = categories.GetActiveAsync(cancellationToken);
        var userId = GetUserId();
        var count = userId is null ? 0 : (await basket.GetAsync(userId.Value, cancellationToken)).Items.Sum(item => item.Quantity);
        return View(new HeaderComponentViewModel { Categories = await categoryTask, BasketItemCount = count });
    }

    private int? GetUserId()
    {
        var value = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out var id) && id > 0 ? id : null;
    }
}
