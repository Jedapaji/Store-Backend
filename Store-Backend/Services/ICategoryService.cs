using Store_Backend.DTOs;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service interface for Category business logic operations.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>List of category DTOs.</returns>
        Task<List<CategoryDto>> GetAllCategoriesAsync();

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>Category DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when category is not found.</exception>
        Task<CategoryDto> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="createCategoryDto">Category creation data.</param>
        /// <returns>Created category DTO.</returns>
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="updateCategoryDto">Category update data.</param>
        /// <returns>Updated category DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when category is not found.</exception>
        Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when category is not found.</exception>
        Task DeleteCategoryAsync(int id);
    }
}
