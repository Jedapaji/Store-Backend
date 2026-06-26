using Store_Backend.DTOs;
using Store_Backend.Models;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service interface for Order business logic operations.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>List of order DTOs.</returns>
        Task<List<OrderDto>> GetAllOrdersAsync();

        /// <summary>
        /// Retrieves an order by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>Order DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when order is not found.</exception>
        Task<OrderDto> GetOrderByIdAsync(int id);

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="createOrderDto">Order creation data.</param>
        /// <returns>Created order DTO.</returns>
        /// <exception cref="ArgumentException">Thrown when customer does not exist or invalid data.</exception>
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <param name="updateOrderDto">Order update data.</param>
        /// <returns>Updated order DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when order is not found.</exception>
        /// <exception cref="ArgumentException">Thrown when customer does not exist or invalid data.</exception>
        Task<OrderDto> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto);

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when order is not found.</exception>
        Task DeleteOrderAsync(int id);

        /// <summary>
        /// Retrieves all orders by customer.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>List of order DTOs for the customer.</returns>
        Task<List<OrderDto>> GetOrdersByCustomerAsync(int customerId);

        /// <summary>
        /// Creates a new purchase order with multiple items and manages stock.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="purchaseOrder">Purchase order with items.</param>
        /// <returns>Created order DTO.</returns>
        /// <exception cref="ArgumentException">Thrown when customer does not exist, product not found, or insufficient stock.</exception>
        Task<OrderDto> CreatePurchaseOrderAsync(int customerId, PurchaseOrder purchaseOrder);
    }
}
