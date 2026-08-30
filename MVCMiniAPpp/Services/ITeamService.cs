using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface ITeamService
{
    Task<IReadOnlyList<Team>> GetActiveAsync(CancellationToken cancellationToken = default);
}
