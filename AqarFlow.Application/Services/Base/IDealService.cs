
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services.Base
{
    public interface IDealService
    {
        IEnumerable<Deal> GetAll();

        Deal? GetById(int id);

        void Add(Deal deal);

        void Update(Deal deal);

        void Delete(Deal deal);
    }
}
