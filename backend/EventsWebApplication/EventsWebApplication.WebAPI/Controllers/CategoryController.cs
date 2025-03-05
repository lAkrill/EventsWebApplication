using EventsWebApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApplication.WebAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(CancellationToken ct = default)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(ct);
            return Ok(categories);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken ct = default)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, ct);
            return Ok(category);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto categoryCreateDto, CancellationToken ct = default)
        {
            var category = await _categoryService.CreateCategoryAsync(categoryCreateDto, ct);
            return Ok(category);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto, CancellationToken ct = default)
        {
            categoryUpdateDto.Id = id;
            var category = await _categoryService.UpdateCategoryAsync(categoryUpdateDto, ct);
            return Ok(category);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken ct = default)
        {
            await _categoryService.DeleteCategoryByIdAsync(id, ct);
            return Ok();
        }
    }
}
