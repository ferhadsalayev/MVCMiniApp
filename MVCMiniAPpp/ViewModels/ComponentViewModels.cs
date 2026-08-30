using MVCMiniAPpp.Helpers;
using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.ViewModels;

public sealed class HomeComponentViewModel
{
    public IReadOnlyList<Slider> Sliders { get; init; } = [];
    public IReadOnlyList<Product> FeaturedProducts { get; init; } = [];
    public IReadOnlyList<Team> TeamMembers { get; init; } = [];
    public IReadOnlyList<Testimonial> Testimonials { get; init; } = [];
}

public sealed class HeaderComponentViewModel
{
    public IReadOnlyList<Category> Categories { get; init; } = [];
    public int BasketItemCount { get; init; }
}

public sealed class BlogComponentViewModel
{
    public PaginatedList<Blog> Posts { get; init; } = null!;
}
