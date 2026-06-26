namespace Store_Backend.DTOs
{
    /// <summary>
    /// DTO for updating an existing order.
    /// </summary>
    public class UpdateOrderDto
    {
        /// <summary>
        /// Customer identifier.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Order state/date.
        /// </summary>
        public DateTime? OrderState { get; set; }

        /// <summary>
        /// Total order amount.
        /// </summary>
        public decimal? TotalAmount { get; set; }
    }
}
