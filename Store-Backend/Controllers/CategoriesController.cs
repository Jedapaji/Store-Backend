using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <response code="200">Ok</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get category by id.
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Modify category by id.
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="updateCategoryDto">Category update data</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> PutCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            return Ok(category);
        }

        // POST: api/Categories
        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="createCategoryDto">Category creation data</param>
        /// <response code="201">Created</response>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CreateCategoryDto createCategoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <response code="204">Not Content</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
