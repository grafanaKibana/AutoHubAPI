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
                .CreateMap<CarColor, CarColorViewModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<CarColorViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<CarColorViewModel>>(_unitOfWork.CarColors.GetAll());
        }

        public CarColorViewModel GetById(int id)
        {
            var carColor = _unitOfWork.CarColors.GetById(id);

            if (carColor == null)
                return null;

            return _mapper.Map<CarColorViewModel>(carColor);
        }

        public CarColorCreateApiModel CreateCarColor(CarColorCreateApiModel carColorModel)
        {
            _unitOfWork.CarColors.Add(new CarColor
            {
                CarColorId = carColorModel.CarColorId,
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