using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarModelModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg
                .CreateMap<CarModel, CarModelViewModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<CarModelViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<CarModelViewModel>>(_unitOfWork.CarModels.GetAll());
        }

        public CarModelViewModel GetById(int id)
        {
            var carModel = _unitOfWork.CarModels.GetById(id);

            if (carModel == null)
                return null;

            return _mapper.Map<CarModelViewModel>(carModel);
        }

        public CarModelCreateApiModel CreateCarModel(CarModelCreateApiModel carModelModel)
        {
            _unitOfWork.CarModels.Add(new CarModel
            {
                CarModelId = carModelModel.CarModelId,
                CarModelName = carModelModel.CarModelName
            });
            _unitOfWork.Commit();
            return carModelModel;
        }

        public bool Exist(string carModelName)
        {
            var carModel = _unitOfWork.CarModels.Find(model => model.CarModelName == carModelName);
            return carModel != null;
        }
    }
}