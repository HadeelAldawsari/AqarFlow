
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services
{
    public class CustomerPropertyInterestService
        : ICustomerPropertyInterestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerPropertyInterestService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CustomerPropertyInterest> GetAll()
        {
            return _unitOfWork.CustomerPropertyInterests.GetAll();
        }

        public CustomerPropertyInterest? GetById(int id)
        {
            return _unitOfWork.CustomerPropertyInterests.GetById(id);
        }

        public void Add(CustomerPropertyInterest interest)
        {
            _unitOfWork.CustomerPropertyInterests.Add(interest);
            _unitOfWork.Complete();
        }

        public void Update(CustomerPropertyInterest interest)
        {
            _unitOfWork.CustomerPropertyInterests.Update(interest);
            _unitOfWork.Complete();
        }

        public void Delete(CustomerPropertyInterest interest)
        {
            _unitOfWork.CustomerPropertyInterests.Delete(interest);
            _unitOfWork.Complete();
        }
    }
}
