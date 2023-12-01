using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoSurvey.Models;

namespace DojoSurvey.Controllers;

public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Form()
        {
            return View();
        }

        [HttpGet("survey")]
        public IActionResult ReturnedData()
        {
            return View();
        }

        [HttpPost("survey")]
        public IActionResult Returned()
        {
            return View();
        }
    }
