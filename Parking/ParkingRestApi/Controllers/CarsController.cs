using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingCore;
using ParkingCore.Interfaces;

namespace ParkingRestApi.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly IParking _parking;
        public CarsController(IParking parking) => _parking = parking;

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_parking.GetCars());
        }

        [HttpGet("{carId}")]
        public IActionResult Get(Guid carId)
        {
            return Json(_parking.GetCar(carId));
        }

        [HttpPost]
        public IActionResult Post(Car car)
        {
            try
            {
                _parking.AddCar(car);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid carId)
        {
            try
            {
                _parking.RemoveCar(carId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}