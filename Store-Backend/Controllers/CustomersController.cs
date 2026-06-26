using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        /// <summary>
        /// Get all customers.
        /// </summary>
        /// <response code="200">Ok</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/Customers/5
        /// <summary>
        /// Get customer by id.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return Ok(customer);
        }

        // PUT: api/Customers/5
        /// <summary>
        /// Modify customer by id.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="updateCustomerDto">Customer update data</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> PutCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerService.UpdateCustomerAsync(id, updateCustomerDto);
            return Ok(customer);
        }

        // POST: api/Customers
        /// <summary>
        /// Create a new customer.
        /// </summary>
        /// <param name="createCustomerDto">Customer creation data</param>
        /// <response code="201">Created</response>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = await _customerService.CreateCustomerAsync(createCustomerDto);
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// Delete a customer.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <response code="204">Not Content</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
