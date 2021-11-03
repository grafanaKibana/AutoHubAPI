using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarModelDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CarModelResponseDTO> GetAll()
        {
            var models = _unitOfWork.CarModels.GetAll();
            var mappedModels = _mapper.Map<IEnumerable<CarModelResponseDTO>>(models);
            return mappedModels;
        }

        public CarModelResponseDTO GetById(int carBrandId)
        {
            var model = _unitOfWork.CarModels.GetById(carBrandId);
            var mappedModels = _mapper.Map<CarModelResponseDTO>(model);
            return mappedModels;
        }

        public void Create(CarModelCreateRequestDTO createModelDTO)
        {
            var model = _mapper.Map<CarModel>(createModelDTO);
            _unitOfWork.CarModels.Add(model);
            _unitOfWork.Commit();
        }

        public void Update(int carModelId, CarModelUpdateRequestDTO updateModelDTO)
        {
            var carModel = _unitOfWork.CarModels.GetById(carModelId);
            carModel.CarModelName = updateModelDTO.CarModelName;

            _unitOfWork.CarModels.Update(carModel);
            _unitOfWork.Commit();
        }
    }
}