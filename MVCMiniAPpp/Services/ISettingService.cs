using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface ISettingService
{
    Task<IReadOnlyList<Setting>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Setting?> GetAsync(string key, CancellationToken cancellationToken = default);
    Task<Setting> SaveAsync(string key, string value, CancellationToken cancellationToken = default);
}
