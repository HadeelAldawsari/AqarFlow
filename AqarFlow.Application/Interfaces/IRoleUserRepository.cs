
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface IRoleUserRepository
    {
        List<RoleUser> GetByUserId(int userId);

        void Add(RoleUser roleUser);

        void DeleteByUserId(int userId);

        void Save();
    }
}
