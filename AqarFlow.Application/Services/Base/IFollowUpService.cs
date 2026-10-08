
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services.Base
{
    public interface IFollowUpService
    {
        IEnumerable<FollowUp> GetAll();

        FollowUp? GetById(int id);

        void Add(FollowUp followUp);

        void Update(FollowUp followUp);

        void Delete(FollowUp followUp);
    }
}
