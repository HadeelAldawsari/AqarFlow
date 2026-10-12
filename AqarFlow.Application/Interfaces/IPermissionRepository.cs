
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface IPermissionRepository
    {
        List<Permission> GetAll();

        Permission? GetById(int id);

        void Add(Permission permission);

        void Update(Permission permission);

        void Delete(Permission permission);

        void Save();
    }
}
