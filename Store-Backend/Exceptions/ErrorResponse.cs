namespace Store_Backend.Exceptions
{
    /// <summary>
    /// Response model for error handling.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Detailed error description (only in development).
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Error timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
