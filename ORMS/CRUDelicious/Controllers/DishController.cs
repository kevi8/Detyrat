using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace CRUDelicious.Controllers;

public class DishController : Controller
{
      private readonly ILogger<DishController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;         
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public DishController(ILogger<DishController> logger, MyContext context)    
    {        
        _logger = logger;
        // When our DishController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;    
    }  

    [HttpGet("")]    
    public IActionResult Index()    
    {     
        // Now any time we want to access our database we use _context   
        List<Dish> AllDishes = _context.Dishes.ToList();
        return View(AllDishes);    
    } 

    // Inside DishController
[HttpGet("/new")]
public IActionResult New()
{

    return View();
}

[HttpPost("/create")]
public IActionResult Create(Dish ngaForma)
{

    if (ModelState.IsValid)
    {
        _context.Add(ngaForma);
        _context.SaveChanges();
        return RedirectToAction("Index");
        
    }else
    {
        return View(New);
    }
}
[HttpGet("/details/{itemId}")]
public IActionResult Details(int itemId)
{
    Dish item = _context.Dishes.FirstOrDefault(e=>e.DishId == itemId);

    return View(item);
} 

[HttpGet("/edit/{itemId}")]
public IActionResult Edit(int itemId)
{
    Dish item = _context.Dishes.FirstOrDefault(e=>e.DishId == itemId);

    return View(item);
} 

[HttpGet("/delete/{itemId}")]
public IActionResult Delete(int itemId)
{
    Dish item = _context.Dishes.FirstOrDefault(e=>e.DishId == itemId);
    _context.Remove(item);
    _context.SaveChanges();

    return RedirectToAction("Index");
} 
[HttpPost("/update/{itemId}")]
public object Update(Dish marrNgaForma,int itemId)
{
    Dish item = _context.Dishes.FirstOrDefault(e=>e.DishId == itemId);
    if (ModelState.IsValid)
    {
        item.Testines=marrNgaForma.Testines;
        item.Name=marrNgaForma.Name;
        item.Chef=marrNgaForma.Chef;
        item.Description=marrNgaForma.Description;
        item.Calories=marrNgaForma.Calories;
        item.UpdatedAt=DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    return View("Edit",item);
} 

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
