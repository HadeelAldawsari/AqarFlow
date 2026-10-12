
using AqarFlow.Application.DTOs;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
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
