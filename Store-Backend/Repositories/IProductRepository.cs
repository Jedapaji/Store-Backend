using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository interface for Product data access operations.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>List of all products.</returns>
        Task<List<Product>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>Product if found; otherwise null.</returns>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">Product to add.</param>
        /// <returns>The added product with generated identifier.</returns>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">Product with updated values.</param>
        /// <returns>The updated product.</returns>
        Task<Product> UpdateAsync(Product product);

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>True if product was deleted; false if not found.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Checks if a product exists by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>True if product exists; otherwise false.</returns>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Retrieves all products by category identifier.
        /// </summary>
        /// <param name="categoryId">Category identifier.</param>
        /// <returns>List of products in the category.</returns>
        Task<List<Product>> GetByCategoryAsync(int categoryId);
    }
}
