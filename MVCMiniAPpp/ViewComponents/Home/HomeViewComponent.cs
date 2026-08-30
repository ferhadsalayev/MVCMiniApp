using Microsoft.AspNetCore.Mvc;
using MVCMiniAPpp.Models;
using MVCMiniAPpp.Services;
using MVCMiniAPpp.ViewModels;

namespace MVCMiniAPpp.ViewComponents.Home;

public sealed class HomeViewComponent(
    ISliderService sliders,
    IProductService products,
    ITeamService team,
    ITestimonialService testimonials) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        var slidersTask = sliders.GetActiveAsync(cancellationToken);
        var productsTask = products.GetFeaturedAsync(cancellationToken: cancellationToken);
        var teamTask = team.GetActiveAsync(cancellationToken);
        var testimonialsTask = testimonials.GetActiveAsync(cancellationToken);
        await Task.WhenAll(slidersTask, productsTask, teamTask, testimonialsTask);

        return View(new HomeComponentViewModel
        {
            Sliders = await slidersTask,
            FeaturedProducts = await productsTask,
            TeamMembers = await teamTask,
            Testimonials = await testimonialsTask
        });
    }
}
