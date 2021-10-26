using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.BLL.Services
{
    public class CarColorService : ICarColorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CarColor> GetAll()
        {
            return _unitOfWork.CarColors.GetAll();
        }

        public CarColor GetById(int id)
        {
            var carColor = _unitOfWork.CarColors.GetById(id);
            return carColor;
        }

        public CarColor CreateCarColor(CarColor carColorModel)
        {
            _unitOfWork.CarColors.Add(carColorModel);
            _unitOfWork.Commit();
            return carColorModel;
        }

        public CarColor UpdateCarColor(CarColor carColorModel)
        {
            var carColor = _unitOfWork.CarColors.GetById(carColorModel.CarColorId);
            carColor.CarColorName = carColorModel.CarColorName;

            _unitOfWork.CarColors.Update(carColor);
            _unitOfWork.Commit();

            return carColorModel;
        }

        public bool Exist(string carColorName)
        {
            return _unitOfWork.CarColors.Any(color => color.CarColorName == carColorName);
        }
    }
}