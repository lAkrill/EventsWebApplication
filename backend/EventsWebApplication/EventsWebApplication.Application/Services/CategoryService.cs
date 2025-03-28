using AutoMapper;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Core.Interfaces;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(Guid id, CancellationToken ct)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id, ct);
            if (category == null)
            {
                throw new NotFoundException($"Category not found with id: {id}");
            }

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<List<CategoryReadDto>> GetAllCategoriesAsync(CancellationToken ct)
        {
            var categoryList = await _categoryRepository.GetAllCategoriesAsync(ct);
            return _mapper.Map<List<CategoryReadDto>>(categoryList);
        }

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto, CancellationToken ct = default)
        {
            if (categoryCreateDto == null)
            {
                throw new ArgumentNullException(nameof(categoryCreateDto));
            }

            var existingCategory = await _categoryRepository.GetCategoryByTitleAsync(categoryCreateDto.Title, ct);
            if (existingCategory != null)
            {
                throw new NotUniqueException($"Category with title {categoryCreateDto.Title} already exists");
            }

            var category = _mapper.Map<Category>(categoryCreateDto);
            
            await _categoryRepository.AddCategoryAsync(category, ct);
            
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto, CancellationToken ct = default)
        {
            if (categoryUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(categoryUpdateDto));
            }
            
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(categoryUpdateDto.Id, ct);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Category not found with id: {categoryUpdateDto.Id}");
            }

            var newCategory = _mapper.Map(categoryUpdateDto, existingCategory);
            await _categoryRepository.UpdateCategoryAsync(newCategory, ct);
            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task DeleteCategoryByIdAsync(Guid id, CancellationToken ct)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id, ct);
            if (category == null)
            {
                throw new NotFoundException($"Category not found with id: {id}");
            }

            await _categoryRepository.DeleteCategoryAsync(category, ct);
        }
    }
}
