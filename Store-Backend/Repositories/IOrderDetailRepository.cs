using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository interface for OrderDetail data access operations.
    /// </summary>
    public interface IOrderDetailRepository
    {
        /// <summary>
        /// Retrieves all order details from the database.
        /// </summary>
        /// <returns>List of all order details.</returns>
        Task<List<OrderDetail>> GetAllAsync();

        /// <summary>
        /// Retrieves an order detail by its identifier.
        /// </summary>
        /// <param name="id">OrderDetail identifier.</param>
        /// <returns>OrderDetail if found; otherwise null.</returns>
        Task<OrderDetail?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new order detail to the database.
        /// </summary>
        /// <param name="orderDetail">OrderDetail to add.</param>
        /// <returns>The added order detail with generated identifier.</returns>
        Task<OrderDetail> AddAsync(OrderDetail orderDetail);

        /// <summary>
        /// Updates an existing order detail.
        /// </summary>
        /// <param name="orderDetail">OrderDetail with updated values.</param>
        /// <returns>The updated order detail.</returns>
        Task<OrderDetail> UpdateAsync(OrderDetail orderDetail);

        /// <summary>
        /// Deletes an order detail by its identifier.
        /// </summary>
        /// <param name="id">OrderDetail identifier.</param>
        /// <returns>True if order detail was deleted; false if not found.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Checks if an order detail exists by its identifier.
        /// </summary>
        /// <param name="id">OrderDetail identifier.</param>
        /// <returns>True if order detail exists; otherwise false.</returns>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Retrieves all order details by order identifier.
        /// </summary>
        /// <param name="orderId">Order identifier.</param>
        /// <returns>List of order details for the order.</returns>
        Task<List<OrderDetail>> GetByOrderAsync(int orderId);
    }
}
