using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Models;

namespace ViewModel.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        string message = "This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. This is a really long message. ";
        return View("Index", message);
    }

    [HttpGet("numbers")]
    public IActionResult Numbers()
    {
        int[] numbers = new int[] {1,2,10,21,8,7,3};
        return View(numbers);
    }

    [HttpGet("User")]
    public IActionResult OneUser()
    {
        User user = new User()
        {
            FirstName = "Kevi",
            LastName = "Likollari"
        };
        return View(user);
    }

    [HttpGet("Users")]
    public IActionResult AllUsers()
    {
        List<User> AllUsers = new List<User>();
        AllUsers.Add(new User(){FirstName = "User1", LastName = "User1"});
        AllUsers.Add(new User(){FirstName = "User2", LastName = "User2"});
        AllUsers.Add(new User(){FirstName = "User3", LastName = "User3"});
        AllUsers.Add(new User(){FirstName = "User4", LastName = "User4"});
        AllUsers.Add(new User(){FirstName = "User5", LastName = "User5"});
        return View(AllUsers);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}