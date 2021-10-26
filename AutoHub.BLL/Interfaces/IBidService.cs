using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        Bid GetById(int id);
        Bid CreateBid(Bid bidModel);
    }
}