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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        /// <summary>
        /// Get all products.
        /// </summary>
        /// <response code="200">Ok</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }



        // GET: api/Products/5
        /// <summary>
        /// Get products by id.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        // PUT: api/Products/5
        /// <summary>
        /// Modify product by id.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="updateProductDto">Product update data</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> PutProduct(int id, UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(id, updateProductDto);
            return Ok(product);
        }

        // POST: api/Products
        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="createProductDto">Product creation data</param>
        /// <response code="201">Created</response>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto createProductDto)
        {
            var product = await _productService.CreateProductAsync(createProductDto);
            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <response code="204">Not Content</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
