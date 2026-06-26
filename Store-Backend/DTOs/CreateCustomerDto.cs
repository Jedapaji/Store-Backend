namespace Store_Backend.DTOs
{
    /// <summary>
    /// DTO for creating a new customer.
    /// </summary>
    public class CreateCustomerDto
    {
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
