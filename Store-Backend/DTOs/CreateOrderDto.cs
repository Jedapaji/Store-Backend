namespace Store_Backend.DTOs
{
    /// <summary>
    /// DTO for creating a new order.
    /// </summary>
    public class CreateOrderDto
    {
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
