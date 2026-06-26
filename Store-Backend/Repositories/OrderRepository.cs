using Microsoft.EntityFrameworkCore;
using Store_Backend.Context;
using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository implementation for Order data access operations.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the OrderRepository class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>List of all orders.</returns>
        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        /// <summary>
        /// Retrieves an order by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>Order if found; otherwise null.</returns>
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        /// <summary>
        /// Adds a new order to the database.
        /// </summary>
        /// <param name="order">Order to add.</param>
        /// <returns>The added order with generated identifier.</returns>
        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="order">Order with updated values.</param>
        /// <returns>The updated order.</returns>
        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        /// <summary>
        /// Deletes an order by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>True if order was deleted; false if not found.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Checks if an order exists by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>True if order exists; otherwise false.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.OrderId == id);
        }

        /// <summary>
        /// Retrieves all orders by customer identifier.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>List of orders for the customer.</returns>
        public async Task<List<Order>> GetByCustomerAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
