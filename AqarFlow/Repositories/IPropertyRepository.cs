using AqarFlow.DTOs;
using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface IPropertyRepository
    {
        // CRUD
        List<PropertyDto> GetAll();

        Property? GetById(int id);

        void Add(Property property);

        void Update(Property property);

        void Delete(Property property);

        void Save();


        // Dashboard
        int Count();
    }
}