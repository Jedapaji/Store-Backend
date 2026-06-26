namespace Store_Backend.Exceptions
{
    /// <summary>
    /// Custom exception for validation errors.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ValidationException class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public ValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ValidationException class.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ValidationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
