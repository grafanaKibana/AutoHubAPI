using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<CarModel> GetAll()
        {
            return _mapper.Map<IEnumerable<CarModel>>(_unitOfWork.Cars.GetAll());
        }

        public CarModel GetById(int id)
        {
            var car = _unitOfWork.Cars.GetById(id);

            if (car == null)
                return null;

            return _mapper.Map<CarModel>(car);
        }
    }
}