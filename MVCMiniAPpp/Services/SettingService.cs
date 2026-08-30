using Microsoft.EntityFrameworkCore;
using MVCMiniAPpp.Models;
using MVCMiniApp.Data;

namespace MVCMiniAPpp.Services;

public sealed class SettingService(AppDbContext db) : ISettingService
{
    public async Task<IReadOnlyList<Setting>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await db.Settings.AsNoTracking().OrderBy(setting => setting.Key).ToListAsync(cancellationToken);

    public Task<Setting?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A setting key is required.", nameof(key));
        return db.Settings.AsNoTracking().SingleOrDefaultAsync(setting => setting.Key == key.Trim(), cancellationToken);
    }

    public async Task<Setting> SaveAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A setting key is required.", nameof(key));
        if (value is null) throw new ArgumentNullException(nameof(value));
        key = key.Trim();
        var setting = await db.Settings.SingleOrDefaultAsync(item => item.Key == key, cancellationToken);
        if (setting is null)
        {
            setting = new Setting { Key = key, Value = value };
            db.Settings.Add(setting);
        }
        else
        {
            setting.Value = value;
            setting.UpdatedAt = DateTime.UtcNow;
        }
        await db.SaveChangesAsync(cancellationToken);
        return setting;
    }
}
