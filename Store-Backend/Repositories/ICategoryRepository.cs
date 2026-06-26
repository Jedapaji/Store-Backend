using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository interface for Category data access operations.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>List of all categories.</returns>
        Task<List<Category>> GetAllAsync();

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>Category if found; otherwise null.</returns>
        Task<Category?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">Category to add.</param>
        /// <returns>The added category with generated identifier.</returns>
        Task<Category> AddAsync(Category category);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">Category with updated values.</param>
        /// <returns>The updated category.</returns>
        Task<Category> UpdateAsync(Category category);

        /// <summary>
        /// Deletes a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>True if category was deleted; false if not found.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Checks if a category exists by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>True if category exists; otherwise false.</returns>
        Task<bool> ExistsAsync(int id);
    }
}
