namespace Store_Backend.DTOs
{
    /// <summary>
    /// DTO for updating an existing customer.
    /// </summary>
    public class UpdateCustomerDto
    {
        /// <summary>
        /// Customer first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Customer last name.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Customer email address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Customer address.
        /// </summary>
        public string? Address { get; set; }
    }
}
