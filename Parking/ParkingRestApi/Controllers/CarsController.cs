using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingCore;

namespace ParkingRestApi.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Get(Car car)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}