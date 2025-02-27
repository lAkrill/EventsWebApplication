using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task AddUserAsync(User user, CancellationToken cancellationToken = default);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    }
}
