using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
        Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default);
        Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default);
        Task DeleteCategoryAsync(Category category, CancellationToken cancellationToken = default);
    }
}
