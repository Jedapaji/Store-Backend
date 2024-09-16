﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_Backend.Context;
using Store_Backend.Models;

namespace Store_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] PurchaseOrder order)
        {
            try
            {
                // Begin a new database transaction
                var transaction = await _context.Database.BeginTransactionAsync();

                // Create a new order with the current date and the total amount received from the PurchaseOrder object
                var newOrder = new Order
                {
                    OrderState = DateTime.Now, // Current date and time
                    TotalAmount = order.Total, // Total amount from PurchaseOrder
                    CustomerId = 1 // Customer ID (always 1 as specified)
                };

                // Add the new order to the database
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync(); // Guardar para generar el OrderId

                // Process each item in the order
                foreach (var item in order.Items)
                {
                    // Retrieve the product from the database
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        // If the product is not found, return a NotFound status
                        return NotFound($"The product with ID {item.ProductId} was not found.");
                    }

                    // Verificar que haya suficiente stock
                    if (product.Stock < item.Quantity)
                    {
                        // If not enough stock, return a BadRequest status
                        return BadRequest($"There is not enough stock for the product {product.Name}.");
                    }

                    // Update the product stock
                    product.Stock -= item.Quantity;

                    // Create a new order detail
                    var orderDetail = new OrderDetail
                    {
                        OrderId = newOrder.OrderId, // Associate the detail with the order
                        ProductId = item.ProductId, 
                        Quantity = item.Quantity,
                        UnitPrice = product.Price // Unit price from the Product table
                    };

                    // Add the order detail to the database
                    _context.OrderDetails.Add(orderDetail);
                }

                // Save all changes, including stock updates
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                // Return a CreatedAtAction result with the newly created order
                return CreatedAtAction("GetOrder", new { id = newOrder.OrderId }, newOrder);
            }
            catch (Exception ex)
            {
                // If an error occurs, roll back the transaction and return a StatusCode 500
                return StatusCode(500, $"An error occurred while processing the order: {ex.Message}");
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
