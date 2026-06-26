namespace Store_Backend.DTOs
{
    /// <summary>
    /// Order Data Transfer Object for API responses.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Order identifier.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Customer identifier.
        /// </summary>
        public required int CustomerId { get; set; }

        /// <summary>
        /// Order state/date.
        /// </summary>
        public required DateTime OrderState { get; set; }

        /// <summary>
        /// Total order amount.
        /// </summary>
        public required decimal TotalAmount { get; set; }
    }
}
