using Store_Backend.DTOs;
using Store_Backend.Models;
using Store_Backend.Repositories;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service implementation for Category business logic operations.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Initializes a new instance of the CategoryService class.
        /// </summary>
        /// <param name="categoryRepository">Category repository.</param>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>List of category DTOs.</returns>
        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToDto).ToList();
        }

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>Category DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when category is not found.</exception>
        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            return MapToDto(category);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="createCategoryDto">Category creation data.</param>
        /// <returns>Created category DTO.</returns>
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (string.IsNullOrWhiteSpace(createCategoryDto.CategoryName))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }

            var category = new Category
            {
                CategoryName = createCategoryDto.CategoryName,
                Description = createCategoryDto.Description
            };

            var createdCategory = await _categoryRepository.AddAsync(category);
            return MapToDto(createdCategory);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="updateCategoryDto">Category update data.</param>
        /// <returns>Updated category DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when category is not found.</exception>
        public async Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            if (!string.IsNullOrWhiteSpace(updateCategoryDto.CategoryName))
            {
                category.CategoryName = updateCategoryDto.CategoryName;
            }

            if (!string.IsNullOrWhiteSpace(updateCategoryDto.Description))
            {
                category.Description = updateCategoryDto.Description;
            }

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            return MapToDto(updatedCategory);
        }

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when category is not found.</exception>
        public async Task DeleteCategoryAsync(int id)
        {
            var deleted = await _categoryRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
        }

        /// <summary>
        /// Maps a Category entity to CategoryDto.
        /// </summary>
        /// <param name="category">Category entity.</param>
        /// <returns>Category DTO.</returns>
        private static CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }
    }
}
