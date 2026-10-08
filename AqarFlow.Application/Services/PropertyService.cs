
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Property> GetAll()
        {
            return _unitOfWork.Properties.GetAll();
        }

        public Property? GetById(int id)
        {
            return _unitOfWork.Properties.GetById(id);
        }

        public void Add(Property property)
        {
            _unitOfWork.Properties.Add(property);
            _unitOfWork.Complete();
        }

        public void Update(Property property)
        {
            _unitOfWork.Properties.Update(property);
            _unitOfWork.Complete();
        }

        public void Delete(Property property)
        {
            _unitOfWork.Properties.Delete(property);
            _unitOfWork.Complete();
        }
    }
}
