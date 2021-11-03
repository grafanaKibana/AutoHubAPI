using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarColorDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarColorService : ICarColorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarColorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CarColorResponseDTO> GetAll()
        {
            var colors = _unitOfWork.CarColors.GetAll();
            var mappedColors = _mapper.Map<IEnumerable<CarColorResponseDTO>>(colors);
            return mappedColors;
        }

        public CarColorResponseDTO GetById(int carColorId)
        {
            var color = _unitOfWork.CarColors.GetById(carColorId);
            var mappedColor = _mapper.Map<CarColorResponseDTO>(color);
            return mappedColor;
        }

        public void Create(CarColorCreateRequestDTO createColorDTO)
        {
            var color = _mapper.Map<CarColor>(createColorDTO);
            _unitOfWork.CarColors.Add(color);
            _unitOfWork.Commit();
        }

        public void Update(int carColorId, CarColorUpdateRequestDTO updateColorDTO)
        {
            var carColor = _unitOfWork.CarColors.GetById(carColorId);
            carColor.CarColorName = updateColorDTO.CarColorName;

            _unitOfWork.CarColors.Update(carColor);
            _unitOfWork.Commit();
        }
    }
}