using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Lot, LotModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<LotModel> GetAll()
        {
            return _mapper.Map<IEnumerable<LotModel>>(_unitOfWork.Lots.GetAll());
        }

        public IEnumerable<LotModel> GetActiveLots()
        {
            return _mapper.Map<IEnumerable<LotModel>>(_unitOfWork.Lots.Find(lot =>
                lot.LotStatusId == LotStatusId.InProgress));
        }

        public LotModel GetById(int id)
        {
            return _mapper.Map<LotModel>(_unitOfWork.Lots.GetById(id));
        }
    }
}