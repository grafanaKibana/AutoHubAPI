using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.BLL.Services
{
    public class BidService : IBidService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BidService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Bid GetById(int id)
        {
            var bid = _unitOfWork.Bids.GetById(id);
            return bid;
        }

        public Bid CreateBid(Bid bidModel)
        {
            _unitOfWork.Bids.Add(bidModel);
            _unitOfWork.Commit();
            return bidModel;
        }
    }
}