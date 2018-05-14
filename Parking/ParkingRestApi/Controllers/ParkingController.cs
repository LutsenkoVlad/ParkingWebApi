using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ParkingRestApi.Controllers
{
    public class ParkingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}