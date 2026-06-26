using Microsoft.EntityFrameworkCore;
using Store_Backend.Context;
using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository implementation for Product data access operations.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the ProductRepository class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>List of all products.</returns>
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>Product if found; otherwise null.</returns>
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">Product to add.</param>
        /// <returns>The added product with generated identifier.</returns>
        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">Product with updated values.</param>
        /// <returns>The updated product.</returns>
        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>True if product was deleted; false if not found.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Checks if a product exists by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>True if product exists; otherwise false.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == id);
        }

        /// <summary>
        /// Retrieves all products by category identifier.
        /// </summary>
        /// <param name="categoryId">Category identifier.</param>
        /// <returns>List of products in the category.</returns>
        public async Task<List<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
