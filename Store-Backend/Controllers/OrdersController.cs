using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_Backend.Context;
using Store_Backend.DTOs;
using Store_Backend.Models;
using Store_Backend.Services;

namespace Store_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <response code="200">Ok</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        // PUT: api/Orders/5
        /// <summary>
        /// Modify order by id.
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="updateOrderDto">Order update data</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> PutOrder(int id, UpdateOrderDto updateOrderDto)
        {
            var order = await _orderService.UpdateOrderAsync(id, updateOrderDto);
            return Ok(order);
        }

        // POST: api/Orders
        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <param name="createOrderDto">Order creation data</param>
        /// <response code="201">Created</response>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(CreateOrderDto createOrderDto)
        {
            var order = await _orderService.CreateOrderAsync(createOrderDto);
            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // POST: api/Orders/CreatePurchaseOrder/{customerId}
        /// <summary>
        /// Create a new purchase order with multiple items and manage stock.
        /// Handles inventory management and uses database transaction for consistency.
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <param name="purchaseOrder">Purchase order with items to process</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request (invalid data, insufficient stock, or product not found)</response>
        /// <response code="404">Not Found (customer not found)</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("CreatePurchaseOrder/{customerId}")]
        public async Task<ActionResult<OrderDto>> CreatePurchaseOrder(int customerId, [FromBody] PurchaseOrder purchaseOrder)
        {
            var order = await _orderService.CreatePurchaseOrderAsync(customerId, purchaseOrder);
            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete an order.
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <response code="204">Not Content</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
