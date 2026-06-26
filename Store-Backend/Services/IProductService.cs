using Store_Backend.DTOs;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service interface for Product business logic operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>List of product DTOs.</returns>
        Task<List<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>Product DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when product is not found.</exception>
        Task<ProductDto> GetProductByIdAsync(int id);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="createProductDto">Product creation data.</param>
        /// <returns>Created product DTO.</returns>
        /// <exception cref="ArgumentException">Thrown when category does not exist or invalid data.</exception>
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="updateProductDto">Product update data.</param>
        /// <returns>Updated product DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when product is not found.</exception>
        /// <exception cref="ArgumentException">Thrown when category does not exist or invalid data.</exception>
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto);

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when product is not found.</exception>
        Task DeleteProductAsync(int id);

        /// <summary>
        /// Retrieves all products in a specific category.
        /// </summary>
        /// <param name="categoryId">Category identifier.</param>
        /// <returns>List of product DTOs in the category.</returns>
        Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    }
}
