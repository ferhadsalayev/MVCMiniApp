using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.Services;

namespace MVCMiniAPpp.ViewComponents.Footer;

public sealed class FooterViewComponent(ISettingService settings) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default) =>
        View(await settings.GetAllAsync(cancellationToken));
}
