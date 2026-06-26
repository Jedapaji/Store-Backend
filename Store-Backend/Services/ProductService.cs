using Store_Backend.DTOs;
using Store_Backend.Models;
using Store_Backend.Repositories;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service implementation for Product business logic operations.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Initializes a new instance of the ProductService class.
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        /// <param name="categoryRepository">Category repository.</param>
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>List of product DTOs.</returns>
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(MapToDto).ToList();
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>Product DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when product is not found.</exception>
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return MapToDto(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="createProductDto">Product creation data.</param>
        /// <returns>Created product DTO.</returns>
        /// <exception cref="ArgumentException">Thrown when category does not exist or invalid data.</exception>
        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            // Validate category exists
            if (!await _categoryRepository.ExistsAsync(createProductDto.CategoryId))
            {
                throw new ArgumentException($"Category with ID {createProductDto.CategoryId} not found.");
            }

            // Validate price and stock
            if (createProductDto.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            if (createProductDto.Stock < 0)
            {
                throw new ArgumentException("Stock cannot be negative.");
            }

            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                CategoryId = createProductDto.CategoryId
            };

            var createdProduct = await _productRepository.AddAsync(product);
            return MapToDto(createdProduct);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="updateProductDto">Product update data.</param>
        /// <returns>Updated product DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when product is not found.</exception>
        /// <exception cref="ArgumentException">Thrown when category does not exist or invalid data.</exception>
        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            // Validate category if being updated
            if (updateProductDto.CategoryId.HasValue && updateProductDto.CategoryId.Value != product.CategoryId)
            {
                if (!await _categoryRepository.ExistsAsync(updateProductDto.CategoryId.Value))
                {
                    throw new ArgumentException($"Category with ID {updateProductDto.CategoryId.Value} not found.");
                }
            }

            // Validate price if being updated
            if (updateProductDto.Price.HasValue && updateProductDto.Price.Value < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            // Validate stock if being updated
            if (updateProductDto.Stock.HasValue && updateProductDto.Stock.Value < 0)
            {
                throw new ArgumentException("Stock cannot be negative.");
            }

            // Update only provided fields
            if (!string.IsNullOrWhiteSpace(updateProductDto.Name))
            {
                product.Name = updateProductDto.Name;
            }

            if (!string.IsNullOrWhiteSpace(updateProductDto.Description))
            {
                product.Description = updateProductDto.Description;
            }

            if (updateProductDto.Price.HasValue)
            {
                product.Price = updateProductDto.Price.Value;
            }

            if (updateProductDto.Stock.HasValue)
            {
                product.Stock = updateProductDto.Stock.Value;
            }

            if (updateProductDto.CategoryId.HasValue)
            {
                product.CategoryId = updateProductDto.CategoryId.Value;
            }

            var updatedProduct = await _productRepository.UpdateAsync(product);
            return MapToDto(updatedProduct);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when product is not found.</exception>
        public async Task DeleteProductAsync(int id)
        {
            var deleted = await _productRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
        }

        /// <summary>
        /// Retrieves all products in a specific category.
        /// </summary>
        /// <param name="categoryId">Category identifier.</param>
        /// <returns>List of product DTOs in the category.</returns>
        public async Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryAsync(categoryId);
            return products.Select(MapToDto).ToList();
        }

        /// <summary>
        /// Maps a Product entity to ProductDto.
        /// </summary>
        /// <param name="product">Product entity.</param>
        /// <returns>Product DTO.</returns>
        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };
        }
    }
}
