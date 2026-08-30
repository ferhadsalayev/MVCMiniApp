using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface ITestimonialService
{
    Task<IReadOnlyList<Testimonial>> GetActiveAsync(CancellationToken cancellationToken = default);
}
