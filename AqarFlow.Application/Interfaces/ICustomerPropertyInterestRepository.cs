
using AqarFlow.Application.DTOs;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
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
