using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository interface for Order data access operations.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>List of all orders.</returns>
        Task<List<Order>> GetAllAsync();

        /// <summary>
        /// Retrieves an order by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>Order if found; otherwise null.</returns>
        Task<Order?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new order to the database.
        /// </summary>
        /// <param name="order">Order to add.</param>
        /// <returns>The added order with generated identifier.</returns>
        Task<Order> AddAsync(Order order);

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="order">Order with updated values.</param>
        /// <returns>The updated order.</returns>
        Task<Order> UpdateAsync(Order order);

        /// <summary>
        /// Deletes an order by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>True if order was deleted; false if not found.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Checks if an order exists by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>True if order exists; otherwise false.</returns>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Retrieves all orders by customer identifier.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>List of orders for the customer.</returns>
        Task<List<Order>> GetByCustomerAsync(int customerId);
    }
}
