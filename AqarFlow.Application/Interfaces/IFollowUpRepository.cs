
using AqarFlow.Application.DTOs;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces
{
    public interface IFollowUpRepository
    {
        List<FollowUpDto> GetAll();

        FollowUp? GetById(int id);

        void Add(FollowUp followUp);

        void Update(FollowUp followUp);

        void Delete(FollowUp followUp);

        void Save();

        int CountForDate(DateTime date);

        List<FollowUp> GetUpcoming(int count);
    }
}
