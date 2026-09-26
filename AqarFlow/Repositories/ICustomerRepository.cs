using AqarFlow.DTOs;
using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface ICustomerRepository
    {
        // CRUD
        List<CustomerDto> GetAll();

        Customer? GetById(int id);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(Customer customer);

        void Save();


        // Dashboard
        int Count();

        int CountByStatus(string status);

        List<Customer> GetRecent(int count);

        int[] GetMonthlyCounts(int year);
    }
}