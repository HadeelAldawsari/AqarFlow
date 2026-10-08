
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Services
{
    public class DealService : IDealService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DealService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // GET ALL DEALS
        // =========================
        public IEnumerable<Deal> GetAll()
        {
            return _unitOfWork.Deals.GetAll();
        }

        // =========================
        // GET DEAL BY ID
        // =========================
        public Deal? GetById(int id)
        {
            return _unitOfWork.Deals.GetById(id);
        }

        // =========================
        // ADD DEAL
        // =========================
        public void Add(Deal deal)
        {
            _unitOfWork.Deals.Add(deal);
            _unitOfWork.Complete();
        }

        // =========================
        // UPDATE DEAL
        // =========================
        public void Update(Deal deal)
        {
            _unitOfWork.Deals.Update(deal);
            _unitOfWork.Complete();
        }

        // =========================
        // DELETE DEAL
        // =========================
        public void Delete(Deal deal)
        {
            _unitOfWork.Deals.Delete(deal);
            _unitOfWork.Complete();
        }
    }
}
