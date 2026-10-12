
using AqarFlow.Application.DTOs;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface ICustomerRepository
    {
        List<CustomerDto> GetAll();

        Customer? GetById(int id);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(Customer customer);

        void Save();

        int Count();

        int CountByStatus(string status);

        List<Customer> GetRecent(int count);

        int[] GetMonthlyCounts(int year);
    }
}
