using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using LoginRegistration.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LoginRegistration.Controllers;

public class HomeController : Controller
{
     private readonly ILogger<HomeController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;         
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public HomeController(ILogger<HomeController> logger, MyContext context)    
    {        
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;    
    }
    [SessionCheck]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost("Create")]
    public IActionResult CreateUser(User userFromView){
        if (ModelState.IsValid)
        {
            _context.Add(userFromView);
            _context.SaveChanges();
            return RedirectToAction("SingIn");
        }
        return View("Index");
    }

    public IActionResult SingIn()
    {
        
        return View();
    }
    
    [HttpPost("Login")]
    public IActionResult Login(SingIn userSubmission){

        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.SEmail);        
        // If no user ex
        if (userInDb == null)
        {   
            ModelState.AddModelError("Email", "Invalid Email");            
            return View("Auth"); 
        }
        PasswordHasher<SingIn> hasher = new PasswordHasher<SingIn>();                    
        // Verify provided password against hash stored in db        
        var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.SPassword);   
         if(result == 0)        
        {            
             ModelState.AddModelError("Password", "Invalid Password");            
            return View("Auth");       
        }
        int UserId = userInDb.UserId;
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        return RedirectToAction("Succes");

            
        }
        

        return View("Auth"); 
    }
    public IActionResult Succes()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

internal class SessionCheckAttribute : Attribute
{
}