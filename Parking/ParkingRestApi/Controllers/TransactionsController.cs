using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingCore.Interfaces;

namespace ParkingRestApi.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IParking _parking;
        public TransactionsController(IParking parking) => _parking = parking;

        [HttpGet]
        public IActionResult GetLog()
        {
            return Json(_parking.ShowLog());
        }

        [HttpGet]
        [Route("GetTransactionsForLastMinute")]
        public IActionResult GetTransactionsForLastMinute()
        {
            return Json(_parking.GetTransactionsForLastMinute());
        }

        [HttpGet("{carId}")]
        public IActionResult GetTransactionsForLastMinute(Guid carId)
        {
            return Json(_parking.GetTransactionsForLastMinute(carId));
        }

        [HttpPut]
        [Route("AddCarBalance/{money}/{carId}")]
        public IActionResult AddCarBalance(decimal money, Guid carId)
        {
            try
            {
                _parking.AddMoneyToCar(money, carId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}