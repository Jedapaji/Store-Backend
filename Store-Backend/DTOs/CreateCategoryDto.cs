namespace Store_Backend.DTOs
{
    /// <summary>
    /// DTO for creating a new category.
    /// </summary>
    public class CreateCategoryDto
    {
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
