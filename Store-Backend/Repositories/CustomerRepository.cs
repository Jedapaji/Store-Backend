using Microsoft.EntityFrameworkCore;
using Store_Backend.Context;
using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository implementation for Customer data access operations.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the CustomerRepository class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>List of all customers.</returns>
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Retrieves a customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>Customer if found; otherwise null.</returns>
        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer">Customer to add.</param>
        /// <returns>The added customer with generated identifier.</returns>
        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customer">Customer with updated values.</param>
        /// <returns>The updated customer.</returns>
        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        /// <summary>
        /// Deletes a customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>True if customer was deleted; false if not found.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await GetByIdAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Checks if a customer exists by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>True if customer exists; otherwise false.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerId == id);
        }
    }
}
