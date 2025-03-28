using EventsWebApplication.Core.Interfaces;
using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<Category?> GetCategoryByTitleAsync(string title, CancellationToken ct = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Title == title, ct);
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken ct = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task AddCategoryAsync(Category category, CancellationToken ct = default)
        {
            await _context.Categories.AddAsync(category, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateCategoryAsync(Category category, CancellationToken ct = default)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteCategoryAsync(Category category, CancellationToken ct = default)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(ct);
        }
    }
}
