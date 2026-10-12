
using AqarFlow.Application.DTOs;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface IDealRepository
    {
        List<DealDto> GetAll();

        Deal? GetById(int id);

        void Add(Deal deal);

        void Update(Deal deal);

        void Delete(Deal deal);

        void Save();

        int CountActive();
    }
}
