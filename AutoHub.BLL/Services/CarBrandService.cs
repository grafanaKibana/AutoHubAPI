using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarBrandModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarBrandService : ICarBrandService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarBrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg
                .CreateMap<CarBrand, CarBrandResponseModel>());
            _mapper = new Mapper(mapperConfig);
        }


        public IEnumerable<CarBrandResponseModel> GetAll()
        {
            return _mapper.Map<IEnumerable<CarBrandResponseModel>>(_unitOfWork.CarBrands.GetAll());
        }

        public CarBrandResponseModel GetById(int id)
        {
            var carBrand = _unitOfWork.CarBrands.GetById(id);

            if (carBrand == null)
                return null;

            return _mapper.Map<CarBrandResponseModel>(carBrand);
        }

        public CarBrandCreateRequestModel CreateCarBrand(CarBrandCreateRequestModel carBrandModel)
        {
            _unitOfWork.CarBrands.Add(new CarBrand
            {
                CarBrandId = carBrandModel.CarBrandId,
                CarBrandName = carBrandModel.CarBrandName
            });
            _unitOfWork.Commit();
            return carBrandModel;
        }

        public bool Exist(string carBrandName)
        {
            var carBrand = _unitOfWork.CarBrands.Find(brand => brand.CarBrandName == carBrandName);
            return carBrand != null;
        }
    }
}