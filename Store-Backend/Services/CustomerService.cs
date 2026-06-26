using Store_Backend.DTOs;
using Store_Backend.Models;
using Store_Backend.Repositories;

namespace Store_Backend.Services
{
    /// <summary>
    /// Service implementation for Customer business logic operations.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the CustomerService class.
        /// </summary>
        /// <param name="customerRepository">Customer repository.</param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>List of customer DTOs.</returns>
        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(MapToDto).ToList();
        }

        /// <summary>
        /// Retrieves a customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>Customer DTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when customer is not found.</exception>
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            return MapToDto(customer);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="createCustomerDto">Customer creation data.</param>
        /// <returns>Created customer DTO.</returns>
        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            if (string.IsNullOrWhiteSpace(createCustomerDto.FirstName))
            {
                throw new ArgumentException("First name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(createCustomerDto.LastName))
            {
                throw new ArgumentException("Last name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(createCustomerDto.Email))
            {
                throw new ArgumentException("Email cannot be empty.");
            }

            var customer = new Customer
            {
                FirstName = createCustomerDto.FirstName,
                LastName = createCustomerDto.LastName,
                Email = createCustomerDto.Email,
                Address = createCustomerDto.Address
            };

            var createdCustomer = await _customerRepository.AddAsync(customer);
            return MapToDto(createdCustomer);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <param name="updateCustomerDto">Customer update data.</param>
        /// <returns>Updated customer DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when customer is not found.</exception>
        public async Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            if (!string.IsNullOrWhiteSpace(updateCustomerDto.FirstName))
            {
                customer.FirstName = updateCustomerDto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(updateCustomerDto.LastName))
            {
                customer.LastName = updateCustomerDto.LastName;
            }

            if (!string.IsNullOrWhiteSpace(updateCustomerDto.Email))
            {
                customer.Email = updateCustomerDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(updateCustomerDto.Address))
            {
                customer.Address = updateCustomerDto.Address;
            }

            var updatedCustomer = await _customerRepository.UpdateAsync(customer);
            return MapToDto(updatedCustomer);
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <exception cref="KeyNotFoundException">Thrown when customer is not found.</exception>
        public async Task DeleteCustomerAsync(int id)
        {
            var deleted = await _customerRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
        }

        /// <summary>
        /// Maps a Customer entity to CustomerDto.
        /// </summary>
        /// <param name="customer">Customer entity.</param>
        /// <returns>Customer DTO.</returns>
        private static CustomerDto MapToDto(Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address
            };
        }
    }
}
