
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services
{
    public class FollowUpService : IFollowUpService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowUpService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // GET ALL FOLLOW-UPS
        // =========================
        public IEnumerable<FollowUp> GetAll()
        {
            return _unitOfWork.FollowUps.GetAll();
        }

        // =========================
        // GET FOLLOW-UP BY ID
        // =========================
        public FollowUp? GetById(int id)
        {
            return _unitOfWork.FollowUps.GetById(id);
        }

        // =========================
        // ADD FOLLOW-UP
        // =========================
        public void Add(FollowUp followUp)
        {
            _unitOfWork.FollowUps.Add(followUp);
            _unitOfWork.Complete();
        }

        // =========================
        // UPDATE FOLLOW-UP
        // =========================
        public void Update(FollowUp followUp)
        {
            _unitOfWork.FollowUps.Update(followUp);
            _unitOfWork.Complete();
        }

        // =========================
        // DELETE FOLLOW-UP
        // =========================
        public void Delete(FollowUp followUp)
        {
            _unitOfWork.FollowUps.Delete(followUp);
            _unitOfWork.Complete();
        }
    }
}
