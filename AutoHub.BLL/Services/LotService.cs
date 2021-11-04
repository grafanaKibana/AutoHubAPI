using System;
using System.Collections.Generic;
using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.BLL.Interfaces;
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

        public LotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<LotResponseDTO> GetAll()
        {
            var lots = _unitOfWork.Lots.Include(lot => lot.Car, lot => lot.Creator, lot => lot.Winner);
            var mappedLots = _mapper.Map<IEnumerable<LotResponseDTO>>(lots);
            return mappedLots;
        }

        public IEnumerable<LotResponseDTO> GetActive()
        {
            var lots = _unitOfWork.Lots.Find(lot => lot.LotStatusId == LotStatusEnum.InProgress);
            var mappedLots = _mapper.Map<IEnumerable<LotResponseDTO>>(lots);
            return mappedLots;
        }

        public LotResponseDTO GetById(int lotId)
        {
            var lot = _unitOfWork.Lots.GetById(lotId);
            var mappedLot = _mapper.Map<LotResponseDTO>(lot);
            return mappedLot;
        }

        public void Create(LotCreateRequestDTO createLotDTO)
        {
            var lot = _mapper.Map<Lot>(createLotDTO);

            lot.LotStatusId = LotStatusEnum.New;
            lot.StartTime = DateTime.UtcNow;
            lot.EndTime = lot.StartTime.AddDays(createLotDTO.DurationInDays);

            _unitOfWork.Lots.Add(lot);
            _unitOfWork.Commit();
        }

        public void Update(int lotId, LotUpdateRequestDTO updateLotDTO)
        {
            var lot = _unitOfWork.Lots.GetById(lotId);

            if (updateLotDTO.WinnerId.HasValue)
            {
                var winner = _unitOfWork.Users.GetById(updateLotDTO.WinnerId.Value);
                lot.Winner = winner;
            }

            lot.LotStatusId = (LotStatusEnum)updateLotDTO.LotStatusId;
            lot.EndTime = lot.StartTime.AddDays(updateLotDTO.DurationInDays);

            _unitOfWork.Lots.Update(lot);
            _unitOfWork.Commit();
        }

        public void Delete(int lotId)
        {
            _unitOfWork.Lots.Delete(lotId);
            _unitOfWork.Commit();
        }
    }
}