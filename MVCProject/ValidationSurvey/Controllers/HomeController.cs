﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ValidationSurvey.Models;

namespace ValidationSurvey.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("data")]
    public IActionResult Data(Users user)
    {
        if(ModelState.IsValid){
            return View(user);
        }
        else{
            return View ("Index");
        }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
