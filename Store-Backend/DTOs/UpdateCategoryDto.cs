namespace Store_Backend.DTOs
{
    /// <summary>
    /// DTO for updating an existing category.
    /// </summary>
    public class UpdateCategoryDto
    {
        /// <summary>
        /// Category name.
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Category description.
        /// </summary>
        public string? Description { get; set; }
    }
}
