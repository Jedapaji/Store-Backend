using Microsoft.EntityFrameworkCore;
using Store_Backend.Context;
using Store_Backend.Models;

namespace Store_Backend.Repositories
{
    /// <summary>
    /// Repository implementation for OrderDetail data access operations.
    /// </summary>
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the OrderDetailRepository class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public OrderDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all order details from the database.
        /// </summary>
        /// <returns>List of all order details.</returns>
        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        /// <summary>
        /// Retrieves an order detail by its identifier.
        /// </summary>
        /// <param name="id">OrderDetail identifier.</param>
        /// <returns>OrderDetail if found; otherwise null.</returns>
        public async Task<OrderDetail?> GetByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        /// <summary>
        /// Adds a new order detail to the database.
        /// </summary>
        /// <param name="orderDetail">OrderDetail to add.</param>
        /// <returns>The added order detail with generated identifier.</returns>
        public async Task<OrderDetail> AddAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        /// <summary>
        /// Updates an existing order detail.
        /// </summary>
        /// <param name="orderDetail">OrderDetail with updated values.</param>
        /// <returns>The updated order detail.</returns>
        public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        /// <summary>
        /// Deletes an order detail by its identifier.
        /// </summary>
        /// <param name="id">OrderDetail identifier.</param>
        /// <returns>True if order detail was deleted; false if not found.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var orderDetail = await GetByIdAsync(id);
            if (orderDetail == null)
            {
                return false;
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Checks if an order detail exists by its identifier.
        /// </summary>
        /// <param name="id">OrderDetail identifier.</param>
        /// <returns>True if order detail exists; otherwise false.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.OrderDetails.AnyAsync(od => od.OrderDetailId == id);
        }

        /// <summary>
        /// Retrieves all order details by order identifier.
        /// </summary>
        /// <param name="orderId">Order identifier.</param>
        /// <returns>List of order details for the order.</returns>
        public async Task<List<OrderDetail>> GetByOrderAsync(int orderId)
        {
            return await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();
        }
    }
}
