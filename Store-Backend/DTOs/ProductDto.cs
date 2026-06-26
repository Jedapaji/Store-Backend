namespace Store_Backend.DTOs
{
    /// <summary>
    /// Product Data Transfer Object for API responses.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public required decimal Price { get; set; }

        /// <summary>
        /// Available stock quantity.
        /// </summary>
        public required int Stock { get; set; }

        /// <summary>
        /// Category identifier.
        /// </summary>
        public required int CategoryId { get; set; }
    }
}
