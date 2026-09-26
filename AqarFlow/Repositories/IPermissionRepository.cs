using AqarFlow.Models;

namespace AqarFlow.Repositories
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