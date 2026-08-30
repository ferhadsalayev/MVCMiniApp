using Microsoft.EntityFrameworkCore;
using MVCMiniAPpp.Models;
using MVCMiniApp.Data;

namespace MVCMiniAPpp.Services;

public sealed class SliderService(AppDbContext db) : ISliderService
{
    public async Task<IReadOnlyList<Slider>> GetActiveAsync(CancellationToken cancellationToken = default) =>
        await db.Sliders.AsNoTracking().Where(slider => slider.IsActive)
            .OrderBy(slider => slider.DisplayOrder).ToListAsync(cancellationToken);
}
