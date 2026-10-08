
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services.Base
{
    public interface ICustomerPropertyInterestService
    {
        IEnumerable<CustomerPropertyInterest> GetAll();

        CustomerPropertyInterest? GetById(int id);

        void Add(CustomerPropertyInterest interest);

        void Update(CustomerPropertyInterest interest);

        void Delete(CustomerPropertyInterest interest);
    }
}
