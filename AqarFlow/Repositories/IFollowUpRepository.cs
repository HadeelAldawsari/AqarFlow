using AqarFlow.DTOs;
using AqarFlow.Models;

namespace AqarFlow.Repositories
{
    public interface IFollowUpRepository
    {
        // CRUD
        List<FollowUpDto> GetAll();

        FollowUp? GetById(int id);

        void Add(FollowUp followUp);

        void Update(FollowUp followUp);

        void Delete(FollowUp followUp);

        void Save();


        // Dashboard
        int CountForDate(DateTime date);

        List<FollowUp> GetUpcoming(int count);
    }
}