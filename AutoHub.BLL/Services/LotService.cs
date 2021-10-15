using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.LotModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class LotService : ILotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Lot, LotResponseModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<LotResponseModel> GetAll()
        {
            return _mapper.Map<IEnumerable<LotResponseModel>>(_unitOfWork.Lots.GetAll());
        }

        public IEnumerable<LotResponseModel> GetActiveLots()
        {
            return _mapper.Map<IEnumerable<LotResponseModel>>(_unitOfWork.Lots.Find(lot =>
                lot.LotStatusId == LotStatusId.InProgress));
        }

        public LotResponseModel GetById(int id)
        {
            return _mapper.Map<LotResponseModel>(_unitOfWork.Lots.GetById(id));
        }
    }
}