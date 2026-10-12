
using AqarFlow.Application.DTOs;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface IPropertyRepository
    {
        List<PropertyDto> GetAll();

        Property? GetById(int id);

        void Add(Property property);

        void Update(Property property);

        void Delete(Property property);

        void Save();

        int Count();
    }
}
