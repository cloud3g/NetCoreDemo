using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewApi.AppModel;
using NewApi.Models;
using NewApi.Data;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly NewApiContext _newApiContext;
        public CarAppModel _carAppModel;

        public CarController(NewApiContext context) {
            _newApiContext = context;
            _carAppModel = new CarAppModel(_newApiContext);
        }

        [HttpGet("{id}")]
        public Car Get(Guid id) {
            return _carAppModel.GetCar(id);
        }

        [HttpGet]
        [Route("search/{name}")]
        public Car Get(string name) {
            return _carAppModel.SearchCarByName(name).FirstOrDefault();
        }

        [HttpGet]
        [Route("search/{name}/{color}")]
        public Car Get(string name, string color) {
            return _carAppModel.SearchCarByNameAndColor(name, color).FirstOrDefault();
        }

        [HttpGet]
        public IEnumerable<Car> Get() {
            return _carAppModel.ListCars();
        }

        [HttpPost] 
        public HttpResponseMessage Post(Car car)
        {
            try {
                if(ModelState.IsValid)
                    _carAppModel.AddCar(car);
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);

                return new HttpResponseMessage(HttpStatusCode.OK);

            }catch {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(Car car)
        {
            try {
                if(ModelState.IsValid)
                    _carAppModel.UpdateCar(car);
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            
                return new HttpResponseMessage(HttpStatusCode.OK);
            }catch {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
