using AqarFlow.DTOs;
using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface ICustomerPropertyInterestRepository
    {
        List<CustomerPropertyInterestDto> GetAll();

        CustomerPropertyInterest? GetById(int id);

        void Add(CustomerPropertyInterest interest);

        void Update(CustomerPropertyInterest interest);

        void Delete(CustomerPropertyInterest interest);

        void Save();
    }
}