﻿using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Planer.Models;

namespace Planer.Controllers;
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
    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        ViewBag.myPlans = _context.Plans.Include(e=>e.WeederTwo).Include(e=>e.End).ToList();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet("register")]
    public IActionResult Register(){
        return View("Auth");
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
        return View("Auth");
    }
    [HttpGet("SignIn")]
    public IActionResult SignIn(){
        
        return View("Auth");
    }
    [HttpGet("New")]
    public IActionResult New(){
        
        return View();
    }
    [HttpPost("Login")]
    public IActionResult Login(SignIn userSubmission){

        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.SEmail);        
        // If no user ex
        if (userInDb == null)
        {   
            ModelState.AddModelError("Email", "Invalid Email");            
            return View("Auth"); 
        }
        PasswordHasher<SignIn> hasher = new PasswordHasher<SignIn>();                    
        // Verify provided password against hash stored in db        
        var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.SPassword);   
         if(result == 0)        
        {            
             ModelState.AddModelError("Password", "Invalid Password");            
            return View("Auth");       
        }
        int UserId = userInDb.UserId;
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        return RedirectToAction("Index");

            
        }
        

        return View("Auth"); 
    }
    [HttpGet("LogOut")]
    public IActionResult LogOut(){
        HttpContext.Session.Clear();
        return RedirectToAction("SignIn");
    }

        [HttpPost("New")]
    public IActionResult CreatePlan(Plan PlanFromView){
        if (ModelState.IsValid)
        {
  ;
         _context.Add(PlanFromView);
         _context.SaveChanges();
         return RedirectToAction("Index");   
        }
        return View("New");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
