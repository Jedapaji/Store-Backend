namespace Store_Backend.DTOs
{
    /// <summary>
    /// Category Data Transfer Object for API responses.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Category identifier.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Category name.
        /// </summary>
        public required string CategoryName { get; set; }

        /// <summary>
        /// Category description.
        /// </summary>
        public required string Description { get; set; }
    }
}
