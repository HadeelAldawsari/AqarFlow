
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services.Base
{
    public interface IPropertyService
    {
        IEnumerable<Property> GetAll();

        Property? GetById(int id);

        void Add(Property property);

        void Update(Property property);

        void Delete(Property property);
    }
}
