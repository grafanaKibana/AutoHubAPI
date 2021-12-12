using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.BLL.Exceptions;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoHub.BLL.Services
{
    public class LotService : ILotService
    {
        private readonly AutoHubContext _context;
        private readonly IMapper _mapper;

        public LotService(AutoHubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<LotResponseDTO> GetAll()
        {
            var lots = _context.Lots
                .Include(lot => lot.Car.CarBrand)
                .Include(lot => lot.Car.CarModel)
                .Include(lot => lot.Car.CarColor)
                .Include(lot => lot.Creator.UserRole)
                .Include(lot => lot.Winner.UserRole)
                .Include(lot => lot.LotStatus)
                .ToList();

            var mappedLots = _mapper.Map<IEnumerable<LotResponseDTO>>(lots);
            return mappedLots;
        }

        public IEnumerable<LotResponseDTO> GetActive()
        {
            var lots = _context.Lots
                .Include(lot => lot.Car.CarBrand)
                .Include(lot => lot.Car.CarModel)
                .Include(lot => lot.Car.CarColor)
                .Include(lot => lot.Creator.UserRole)
                .Include(lot => lot.Winner.UserRole)
                .Include(lot => lot.LotStatus)
                .Where(lot => lot.LotStatusId == LotStatusEnum.InProgress)
                .ToList();

            var mappedLots = _mapper.Map<IEnumerable<LotResponseDTO>>(lots);
            return mappedLots;
        }

        public LotResponseDTO GetById(int lotId)
        {
            var lot = _context.Lots
                .Include(lot => lot.Car.CarBrand)
                .Include(lot => lot.Car.CarModel)
                .Include(lot => lot.Car.CarColor)
                .Include(lot => lot.Creator.UserRole)
                .Include(lot => lot.Winner.UserRole)
                .Include(lot => lot.LotStatus)
                .FirstOrDefault(lot => lot.LotId == lotId);

            if (lot == null) throw new NotFoundException($"Lot with ID {lotId} not exist");

            var mappedLot = _mapper.Map<LotResponseDTO>(lot);
            return mappedLot;
        }

        public void Create(LotCreateRequestDTO createLotDTO)
        {
            var lot = _mapper.Map<Lot>(createLotDTO);

            lot.LotStatusId = LotStatusEnum.New;
            lot.StartTime = DateTime.UtcNow;
            lot.EndTime = lot.StartTime.AddDays(createLotDTO.DurationInDays);

            _context.Lots.Add(lot);
            _context.SaveChanges();
        }

        public void Update(int lotId, LotUpdateRequestDTO updateLotDTO)
        {
            if (!Enum.IsDefined(typeof(LotStatusEnum), updateLotDTO.LotStatusId))
                throw new EntityValidationException("Incorrect lot status ID");

            var lot = _context.Lots
                .Include(lot => lot.Car.CarBrand)
                .Include(lot => lot.Car.CarModel)
                .Include(lot => lot.Car.CarColor)
                .Include(lot => lot.Creator.UserRole)
                .Include(lot => lot.Winner.UserRole)
                .Include(lot => lot.LotStatus)
                .FirstOrDefault(lot => lot.LotId == lotId);

            if (lot == null) throw new NotFoundException($"Lot with ID {lotId} not exist");

            if (updateLotDTO.WinnerId.HasValue)
            {
                var winner = _context.Users.FirstOrDefault(user => user.UserId == updateLotDTO.WinnerId);

                if (winner == null)
                    throw new NotFoundException($"User with ID {updateLotDTO.WinnerId} not exist");

                lot.Winner = winner;
            }

            lot.LotStatusId = (LotStatusEnum)updateLotDTO.LotStatusId;
            lot.EndTime = lot.StartTime.AddDays(updateLotDTO.DurationInDays);

            _context.Lots.Update(lot);
            _context.SaveChanges();
        }

        public void Delete(int lotId)
        {
            var lot = _context.Lots.Find(lotId);

            if (lot == null) throw new NotFoundException($"Lot with ID {lotId} not exist");

            _context.Lots.Remove(lot);
            _context.SaveChanges();
        }
    }
}