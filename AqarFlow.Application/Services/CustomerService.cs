
using AqarFlow.Application.DTOs;
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =====================================
        // GET ALL - DTO
        // =====================================

        public IEnumerable<CustomerDto> GetAll()
        {
            return _unitOfWork.Customers.GetAll()
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Purpose = c.Purpose,
                    PropertyType = c.PropertyType,
                    PreferredArea = c.PreferredArea,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt
                })
                .ToList();
        }

        // =====================================
        // GET BY ID - DTO
        // =====================================

        public CustomerDto? GetById(int id)
        {
            var customer = _unitOfWork.Customers.GetById(id);

            if (customer == null)
            {
                return null;
            }

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Purpose = customer.Purpose,
                PropertyType = customer.PropertyType,
                PreferredArea = customer.PreferredArea,
                Status = customer.Status,
                CreatedAt = customer.CreatedAt
            };
        }

        // =====================================
        // GET ALL - DOMAIN MODELS
        // =====================================

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _unitOfWork.Customers.GetAll();
        }

        // =====================================
        // GET BY ID - DOMAIN MODEL
        // =====================================

        public Customer? GetCustomerById(int id)
        {
            return _unitOfWork.Customers.GetById(id);
        }

        // =====================================
        // CREATE CUSTOMER
        // =====================================

        public void CreateCustomer(Customer customer)
        {
            customer.Status = "New";
            customer.CreatedAt = DateTime.Now;

            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();
        }

        // =====================================
        // UPDATE CUSTOMER
        // =====================================

        public bool UpdateCustomer(Customer customer)
        {
            var existingCustomer =
                _unitOfWork.Customers.GetById(customer.Id);

            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;
            existingCustomer.Purpose = customer.Purpose;
            existingCustomer.PropertyType = customer.PropertyType;
            existingCustomer.PreferredCity = customer.PreferredCity;
            existingCustomer.PreferredArea = customer.PreferredArea;
            existingCustomer.MinBudget = customer.MinBudget;
            existingCustomer.MaxBudget = customer.MaxBudget;
            existingCustomer.Bedrooms = customer.Bedrooms;
            existingCustomer.Status = customer.Status;
            existingCustomer.Source = customer.Source;
            existingCustomer.Notes = customer.Notes;
            existingCustomer.UpdatedAt = DateTime.Now;

            _unitOfWork.Customers.Update(existingCustomer);
            _unitOfWork.Complete();

            return true;
        }

        // =====================================
        // DELETE CUSTOMER
        // =====================================

        public bool DeleteCustomer(int id)
        {
            var customer = _unitOfWork.Customers.GetById(id);

            if (customer == null)
            {
                return false;
            }

            _unitOfWork.Customers.Delete(customer);
            _unitOfWork.Complete();

            return true;
        }
    }
}
