using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

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


    [HttpPost("dashboard")]
    public IActionResult dashboard(Data data)
    {
        if (ModelState.IsValid)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            HttpContext.Session.SetString("Username", data.username);
#pragma warning restore CS8604 // Possible null reference argument.
            HttpContext.Session.SetInt32("Number", 22);
            return View("dashboard");
        }
        else
        {
            System.Console.WriteLine("Not valid");
            return View("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpPost("increment")]

    public IActionResult increment()
    {
        int currentNumber = HttpContext.Session.GetInt32("Number") ?? 0;
        currentNumber++;
        HttpContext.Session.SetInt32("Number", currentNumber);
        return View("dashboard");

    }

    [HttpPost("decrement")]
    public IActionResult decrement()
    {
        int currentNumber = HttpContext.Session.GetInt32("Number") ?? 0;
        currentNumber--;
        HttpContext.Session.SetInt32("Number", currentNumber);
        return View("dashboard");

    }

    [HttpPost("doublee")]
    public IActionResult doublee()
    {
        int currentNumber = HttpContext.Session.GetInt32("Number") ?? 0;
        currentNumber *= 2;
        HttpContext.Session.SetInt32("Number", currentNumber);
        return View("dashboard");

    }

        [HttpPost("randomize")]
    public IActionResult randomize()
    {
        int currentNumber = HttpContext.Session.GetInt32("Number") ?? 0;
        Random r = new Random();
        currentNumber+= (int)r.Next(1,11);
        HttpContext.Session.SetInt32("Number", currentNumber);
        return View("dashboard");

    }

    [HttpPost("clear")]
    public IActionResult clear(){
        HttpContext.Session.Clear();
        return View("Index");
    }
}