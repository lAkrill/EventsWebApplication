using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category, CancellationToken ct = default);
        Task DeleteCategoryAsync(Guid id, CancellationToken ct = default);
        Task<List<Category>> GetAllCategoriesAsync(CancellationToken ct = default);
        Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct = default);
        Task<Category?> GetCategoryByTitleAsync(string title, CancellationToken ct = default);
        Task UpdateCategoryAsync(Category category, CancellationToken ct = default);
    }
}