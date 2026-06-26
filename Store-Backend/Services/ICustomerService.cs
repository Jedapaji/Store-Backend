using Store_Backend.DTOs;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service interface for Customer business logic operations.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>List of customer DTOs.</returns>
        Task<List<CustomerDto>> GetAllCustomersAsync();

        /// <summary>
        /// Retrieves a customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>Customer DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when customer is not found.</exception>
        Task<CustomerDto> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="createCustomerDto">Customer creation data.</param>
        /// <returns>Created customer DTO.</returns>
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <param name="updateCustomerDto">Customer update data.</param>
        /// <returns>Updated customer DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when customer is not found.</exception>
        Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when customer is not found.</exception>
        Task DeleteCustomerAsync(int id);
    }
}
