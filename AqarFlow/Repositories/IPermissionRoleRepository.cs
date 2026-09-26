using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface IPermissionRoleRepository
    {
        List<int> GetPermissionIdsByRoleId(int roleId);

        void Add(PermissionRole permissionRole);

        void DeleteByRoleId(int roleId);

        void Save();
    }
}