using Microsoft.EntityFrameworkCore;
using MVCMiniAPpp.Models;
using MVCMiniApp.Data;

namespace MVCMiniAPpp.Services;

public sealed class TeamService(AppDbContext db) : ITeamService
{
    public async Task<IReadOnlyList<Team>> GetActiveAsync(CancellationToken cancellationToken = default) =>
        await db.Teams.AsNoTracking().Where(team => team.IsActive)
            .OrderBy(team => team.DisplayOrder).ToListAsync(cancellationToken);
}
