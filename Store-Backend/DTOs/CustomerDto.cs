namespace Store_Backend.DTOs
{
    /// <summary>
    /// Customer Data Transfer Object for API responses.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Customer first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Customer last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Customer email address.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Customer address.
        /// </summary>
        public required string Address { get; set; }
    }
}
