
using AqarFlow.Application.DTOs;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services.Base
{
    public interface ICustomerService
    {
        // Read operations
        IEnumerable<CustomerDto> GetAll();
        CustomerDto? GetById(int id);

        // Customer entity operations
        IEnumerable<Customer> GetAllCustomers();
        Customer? GetCustomerById(int id);

        // Create
        void CreateCustomer(Customer customer);

        // Update
        bool UpdateCustomer(Customer customer);

        // Delete
        bool DeleteCustomer(int id);
    }
}
