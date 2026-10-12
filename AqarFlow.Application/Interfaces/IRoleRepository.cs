
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> GetAll();

        Role? GetById(int id);

        void Add(Role role);

        void Update(Role role);

        void Delete(Role role);

        void Save();
    }
}
