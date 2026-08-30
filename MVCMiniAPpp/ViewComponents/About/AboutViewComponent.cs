using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.Services;

namespace MVCMiniAPpp.ViewComponents.About;

public sealed class AboutViewComponent(ITeamService team) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default) =>
        View(await team.GetActiveAsync(cancellationToken));
}
