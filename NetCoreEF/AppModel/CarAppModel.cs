

using System;
using System.Collections.Generic;
using NewApi.Data;
using NewApi.Models;

namespace NewApi.AppModel {

    public class CarAppModel {
        private readonly NewApiContext _newApiContext;
        private CarRepository _carRepository;

        public CarAppModel(NewApiContext context) {
            _newApiContext = context;
            _carRepository = new CarRepository(_newApiContext);
        }

        public Car GetCar(Guid id) {
            return _carRepository.Get(id);
        }

        public void UpdateCar(Car car) {
            _carRepository.Update(car);
            _carRepository.Commit();
        }

        public void AddCar(Car car) {
            _carRepository.Add(car);
            _carRepository.Commit();
        }

        public IEnumerable<Car> ListCars() {
            return _carRepository.ListAll();
        }

        public IEnumerable<Car> SearchCarByName(string name) {
            return _carRepository.SearchByCriteria(p => p.name.ToUpper().Trim() == name.ToUpper().Trim());
        }

        public IEnumerable<Car> SearchCarByNameAndColor(string name, string color) {
            return _carRepository.SearchByCriteria(p => p.name.ToUpper().Trim() == name.ToUpper().Trim() && p.color == color);
        }
    }

}