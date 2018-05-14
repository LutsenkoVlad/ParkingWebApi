using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingCore.Interfaces;

namespace ParkingRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ParkingController : Controller
    {
        private readonly IParking _parking;
        public ParkingController(IParking parking) => _parking = parking;

        [HttpGet]
        public IActionResult GetFreeSpaces() => Json(_parking.GetFreeParkingSpaces());

        [HttpGet]
        public IActionResult GetOccupiedSpaces() => Json(_parking.GetNotFreeParkingSpaces());

        [HttpGet]
        public IActionResult GetGeneralIncome() => Json(_parking.Balance);
    }
}