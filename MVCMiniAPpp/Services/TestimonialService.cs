using Microsoft.EntityFrameworkCore;
using MVCMiniAPpp.Models;
using MVCMiniApp.Data;

namespace MVCMiniAPpp.Services;

public sealed class TestimonialService(AppDbContext db) : ITestimonialService
{
    public async Task<IReadOnlyList<Testimonial>> GetActiveAsync(CancellationToken cancellationToken = default) =>
        await db.Testimonials.AsNoTracking().Where(testimonial => testimonial.IsActive)
            .OrderBy(testimonial => testimonial.DisplayOrder).ToListAsync(cancellationToken);
}
