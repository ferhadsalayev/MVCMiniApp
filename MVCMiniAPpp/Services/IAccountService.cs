using MVCMiniAPpp.Models;

namespace MVCMiniAPpp.Services;

public interface IAccountService
{
    Task<AppUser> RegisterAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken = default);
    Task<AppUser?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<AppUser?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
}
