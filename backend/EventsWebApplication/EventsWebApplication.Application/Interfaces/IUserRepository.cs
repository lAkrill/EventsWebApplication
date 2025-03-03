using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user, CancellationToken ct = default);
        Task DeleteUserAsync(Guid id, CancellationToken ct = default);
        Task<List<User>> GetAllUsers(CancellationToken ct = default);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetUserByIdAsync(Guid id, CancellationToken ct = default);
        Task UpdateUserAsync(User user, CancellationToken ct = default);
    }
}