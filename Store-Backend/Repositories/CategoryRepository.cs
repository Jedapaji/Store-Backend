using Microsoft.EntityFrameworkCore;
using Store_Backend.Context;
using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository implementation for Category data access operations.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the CategoryRepository class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>List of all categories.</returns>
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>Category if found; otherwise null.</returns>
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">Category to add.</param>
        /// <returns>The added category with generated identifier.</returns>
        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">Category with updated values.</param>
        /// <returns>The updated category.</returns>
        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        /// <summary>
        /// Deletes a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>True if category was deleted; false if not found.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Checks if a category exists by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>True if category exists; otherwise false.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryId == id);
        }
    }
}
