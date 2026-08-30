using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface ISliderService
{
    Task<IReadOnlyList<Slider>> GetActiveAsync(CancellationToken cancellationToken = default);
}
