using Store_Backend.Context;
using Store_Backend.DTOs;
using Store_Backend.Models;
using Store_Backend.Repositories;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service implementation for Order business logic operations.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the OrderService class.
        /// </summary>
        /// <param name="orderRepository">Order repository.</param>
        /// <param name="customerRepository">Customer repository.</param>
        /// <param name="productRepository">Product repository.</param>
        /// <param name="orderDetailRepository">Order detail repository.</param>
        /// <param name="context">Database context for transactions.</param>
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, 
            IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, AppDbContext context)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _context = context;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>List of order DTOs.</returns>
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(MapToDto).ToList();
        }

        /// <summary>
        /// Retrieves an order by its identifier.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <returns>Order DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when order is not found.</exception>
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            return MapToDto(order);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="createOrderDto">Order creation data.</param>
        /// <returns>Created order DTO.</returns>
        /// <exception cref="ArgumentException">Thrown when customer does not exist or invalid data.</exception>
        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            // Validate customer exists
            if (!await _customerRepository.ExistsAsync(createOrderDto.CustomerId))
            {
                throw new ArgumentException($"Customer with ID {createOrderDto.CustomerId} not found.");
            }

            // Validate total amount
            if (createOrderDto.TotalAmount < 0)
            {
                throw new ArgumentException("Total amount cannot be negative.");
            }

            var order = new Order
            {
                CustomerId = createOrderDto.CustomerId,
                OrderState = createOrderDto.OrderState,
                TotalAmount = createOrderDto.TotalAmount
            };

            var createdOrder = await _orderRepository.AddAsync(order);
            return MapToDto(createdOrder);
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <param name="updateOrderDto">Order update data.</param>
        /// <returns>Updated order DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when order is not found.</exception>
        /// <exception cref="ArgumentException">Thrown when customer does not exist or invalid data.</exception>
        public async Task<OrderDto> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            // Validate customer if being updated
            if (updateOrderDto.CustomerId.HasValue && updateOrderDto.CustomerId.Value != order.CustomerId)
            {
                if (!await _customerRepository.ExistsAsync(updateOrderDto.CustomerId.Value))
                {
                    throw new ArgumentException($"Customer with ID {updateOrderDto.CustomerId.Value} not found.");
                }
            }

            // Validate total amount if being updated
            if (updateOrderDto.TotalAmount.HasValue && updateOrderDto.TotalAmount.Value < 0)
            {
                throw new ArgumentException("Total amount cannot be negative.");
            }

            // Update only provided fields
            if (updateOrderDto.CustomerId.HasValue)
            {
                order.CustomerId = updateOrderDto.CustomerId.Value;
            }

            if (updateOrderDto.OrderState.HasValue)
            {
                order.OrderState = updateOrderDto.OrderState.Value;
            }

            if (updateOrderDto.TotalAmount.HasValue)
            {
                order.TotalAmount = updateOrderDto.TotalAmount.Value;
            }

            var updatedOrder = await _orderRepository.UpdateAsync(order);
            return MapToDto(updatedOrder);
        }

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">Order identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when order is not found.</exception>
        public async Task DeleteOrderAsync(int id)
        {
            var deleted = await _orderRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }
        }

        /// <summary>
        /// Retrieves all orders by customer.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>List of order DTOs for the customer.</returns>
        public async Task<List<OrderDto>> GetOrdersByCustomerAsync(int customerId)
        {
            var orders = await _orderRepository.GetByCustomerAsync(customerId);
            return orders.Select(MapToDto).ToList();
        }

        /// <summary>
        /// Creates a new purchase order with multiple items and manages stock.
        /// Uses database transaction to ensure data consistency.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="purchaseOrder">Purchase order with items.</param>
        /// <returns>Created order DTO.</returns>
        /// <exception cref="ArgumentException">Thrown when customer does not exist, product not found, or insufficient stock.</exception>
        public async Task<OrderDto> CreatePurchaseOrderAsync(int customerId, PurchaseOrder purchaseOrder)
        {
            // Validate customer exists
            if (!await _customerRepository.ExistsAsync(customerId))
            {
                throw new ArgumentException($"Customer with ID {customerId} not found.");
            }

            // Validate purchase order
            if (purchaseOrder == null || purchaseOrder.Items == null || purchaseOrder.Items.Count == 0)
            {
                throw new ArgumentException("Purchase order must contain at least one item.");
            }

            // Begin a database transaction for atomic operations
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Create the main order
                var newOrder = new Order
                {
                    OrderState = DateTime.Now,
                    TotalAmount = purchaseOrder.Total,
                    CustomerId = customerId
                };

                // Add the order to the database and save to generate OrderId
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                // Process each item in the purchase order
                decimal calculatedTotal = 0;
                foreach (var item in purchaseOrder.Items)
                {
                    // Retrieve the product from the database
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    if (product == null)
                    {
                        throw new ArgumentException($"The product with ID {item.ProductId} was not found.");
                    }

                    // Validate sufficient stock
                    if (product.Stock < item.Quantity)
                    {
                        throw new ArgumentException($"There is not enough stock for the product '{product.Name}'. Available: {product.Stock}, Requested: {item.Quantity}");
                    }

                    // Update the product stock
                    product.Stock -= item.Quantity;
                    await _productRepository.UpdateAsync(product);

                    // Create order detail for this item
                    var orderDetail = new OrderDetail
                    {
                        OrderId = newOrder.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    };

                    // Add order detail to database
                    _context.OrderDetails.Add(orderDetail);

                    // Accumulate total
                    calculatedTotal += product.Price * item.Quantity;
                }

                // Verify calculated total matches provided total (optional but good practice)
                if (Math.Abs(calculatedTotal - purchaseOrder.Total) > 0.01m)
                {
                    throw new ArgumentException($"Purchase order total mismatch. Expected: {calculatedTotal}, Provided: {purchaseOrder.Total}");
                }

                // Save all order details
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return MapToDto(newOrder);
            }
            catch (Exception)
            {
                // Rollback transaction on any error
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Maps an Order entity to OrderDto.
        /// </summary>
        /// <param name="order">Order entity.</param>
        /// <returns>Order DTO.</returns>
        private static OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderState = order.OrderState,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
