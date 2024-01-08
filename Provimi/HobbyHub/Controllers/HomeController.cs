using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using HobbyHub.Models;

namespace HobbyHub.Controllers;
// Name this anything you want with the word "Attribute" at the end
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if(userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("SignIn", "Home", null);
        }
    }
}



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
    [HttpGet("Hobbie")]
    public IActionResult Hobbie()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        //List<Hobbie> hobbies = _context.Hobbies.ToList();
         ViewBag.Hobby = _context.Hobbies.ToList();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
 
     public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("register")]
    public IActionResult Register(){
        return View("Index");
    }
    [HttpPost("Create")]
    public IActionResult CreateUser(User userFromView){
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();   
            // Updating our newUser's password to a hashed version         
            userFromView.Password = Hasher.HashPassword(userFromView, userFromView.Password);
         _context.Add(userFromView);
         _context.SaveChanges();
         return RedirectToAction("SignIn");   
        }
        return View("Index");
    }
    [HttpGet("SignIn")]
    public IActionResult SignIn(){
        
        return View("Index");
    }
    [HttpPost("Login")]
    public IActionResult Login(SignIn userSubmission){

        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Username == userSubmission.Username);        
        // If no user ex
        if (userInDb == null)
        {   
            ModelState.AddModelError("Email", "Invalid Email");            
            return View("Hobbie"); 
        }
        PasswordHasher<SignIn> hasher = new PasswordHasher<SignIn>();                    
        // Verify provided password against hash stored in db        
        var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.SPassword);   
         if(result == 0)        
        {            
             ModelState.AddModelError("Password", "Invalid Password");            
            return View("Hobbie");       
        }
        int UserId = userInDb.UserId;
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        return RedirectToAction("Hobbie");

            
        }
        return View("Index"); 
    }
    [HttpGet("LogOut")]
    public IActionResult LogOut(){
        HttpContext.Session.Clear();
        return RedirectToAction("SignIn");
    }
    [HttpGet("/Hobbies/New")]
        public IActionResult HobbieCreate()
        {
            return View();
        }
        [HttpPost("/Hobbies/New")]
        public IActionResult CreateHobbie(Hobbie hobby){
        int? userId = HttpContext.Session.GetInt32("UserId");
         if (!ModelState.IsValid)
            {
                return View("Index");
            }
            else if (_context.Hobbies.Any(existingHobby => existingHobby.Name == hobby.Name)){
                ModelState.AddModelError("Name", "Name is already in use!");
                return View("HobbieCreate");
            }
            _context.Add(hobby);
            _context.SaveChanges();
            return RedirectToAction("Hobbie", new 
            {
                hobbyId = hobby.HobbieId
            });    
        }

     [HttpGet("Hobbie/{id}")]
    public IActionResult HobbyInfo(int id){
        Hobbie hobbie = _context.Hobbies.FirstOrDefault(e=>e.HobbieId == id);
        return View(hobbie);

    }
    [HttpGet("Hobbie/{id}/edit")]
    public IActionResult EditHobby(int id){
        Hobbie? hobbie = _context.Hobbies.FirstOrDefault(e=> e.HobbieId == id );
        return View(hobbie);
    }

    [HttpPost("Hobbie/{id}/EditHobby")]
    public IActionResult UpdateHobby(Hobbie hobbie, int id){
        if(ModelState.IsValid){
        Hobbie? editHobbie = _context.Hobbies.FirstOrDefault(e => e.HobbieId == id);
        editHobbie.Name = hobbie.Name;
        editHobbie.Content = hobbie.Content;
        _context.SaveChanges();
        return RedirectToAction("Hobbie");
        }else{
            hobbie.HobbieId = id;
            return View("EditHobby", hobbie);
        }
    }
}
