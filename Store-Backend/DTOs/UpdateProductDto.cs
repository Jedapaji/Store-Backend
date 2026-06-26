namespace Store_Backend.DTOs
{
    /// <summary>
    /// DTO for updating an existing product.
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary>
        /// Product name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Available stock quantity.
        /// </summary>
        public int? Stock { get; set; }

        /// <summary>
        /// Category identifier.
        /// </summary>
        public int? CategoryId { get; set; }
    }
}
