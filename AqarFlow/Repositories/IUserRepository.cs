using AqarFlow.DTOs;
using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface IUserRepository
    {
        List<UserDto> GetAll();

        User? GetById(int id);

        User? GetByEmail(string email);

        void Add(User user);

        void Update(User user);

        void Delete(User user);

        void Save();
    }
}