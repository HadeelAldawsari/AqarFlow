using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface IRoleUserRepository
    {
        List<RoleUser> GetByUserId(int userId);

        void Add(RoleUser roleUser);

        void DeleteByUserId(int userId);

        void Save();
    }
}