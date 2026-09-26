using AqarFlow.DTOs;
using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface IDealRepository
    {
        // CRUD
        List<DealDto> GetAll();

        Deal? GetById(int id);

        void Add(Deal deal);

        void Update(Deal deal);

        void Delete(Deal deal);

        void Save();


        // Dashboard
        int CountActive();
    }
}