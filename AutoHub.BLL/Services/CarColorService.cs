using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarColorModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarColorService : ICarColorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg
                .CreateMap<CarColor, CarColorResponseModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<CarColorResponseModel> GetAll()
        {
            return _mapper.Map<IEnumerable<CarColorResponseModel>>(_unitOfWork.CarColors.GetAll());
        }

        public CarColorResponseModel GetById(int id)
        {
            var carColor = _unitOfWork.CarColors.GetById(id);

            if (carColor == null)
                return null;

            return _mapper.Map<CarColorResponseModel>(carColor);
        }

        public CarColorCreateRequestModel CreateCarColor(CarColorCreateRequestModel carColorModel)
        {
            _unitOfWork.CarColors.Add(new CarColor
            {
                CarColorName = carColorModel.CarColorName
            });
            _unitOfWork.Commit();
            return carColorModel;
        }

        public bool Exist(string carColorName)
        {
            var carColor = _unitOfWork.CarColors.Find(color => color.CarColorName == carColorName);
            return carColor != null;
        }
    }
}