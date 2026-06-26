using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository interface for Customer data access operations.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>List of all customers.</returns>
        Task<List<Customer>> GetAllAsync();

        /// <summary>
        /// Retrieves a customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>Customer if found; otherwise null.</returns>
        Task<Customer?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer">Customer to add.</param>
        /// <returns>The added customer with generated identifier.</returns>
        Task<Customer> AddAsync(Customer customer);

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customer">Customer with updated values.</param>
        /// <returns>The updated customer.</returns>
        Task<Customer> UpdateAsync(Customer customer);

        /// <summary>
        /// Deletes a customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>True if customer was deleted; false if not found.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Checks if a customer exists by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>True if customer exists; otherwise false.</returns>
        Task<bool> ExistsAsync(int id);
    }
}
