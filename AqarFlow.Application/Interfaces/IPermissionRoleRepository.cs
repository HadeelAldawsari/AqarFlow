
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface IPermissionRoleRepository
    {
        List<int> GetPermissionIdsByRoleId(int roleId);

        void Add(PermissionRole permissionRole);

        void DeleteByRoleId(int roleId);

        void Save();
    }
}
