using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user, CancellationToken ct = default);
        Task DeleteUserAsync(User user, CancellationToken ct = default);
        Task<List<User>> GetAllUsersAsync(CancellationToken ct = default);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetUserByIdAsync(Guid id, CancellationToken ct = default);
        Task UpdateUserAsync(User user, CancellationToken ct = default);
    }
}