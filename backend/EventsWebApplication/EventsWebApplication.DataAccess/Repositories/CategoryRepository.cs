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

        public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .ToListAsync(cancellationToken);
        }

        public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCategoryAsync(Category category, CancellationToken cancellationToken = default)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
