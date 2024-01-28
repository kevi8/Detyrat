using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketiIm.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketiIm.Controllers;



public class HomeController : Controller
{
   private readonly ILogger<HomeController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context; 
    private readonly IWebHostEnvironment _environment;        
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public HomeController(ILogger<HomeController> logger, MyContext context, IWebHostEnvironment environment)    
    {        
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;
        _environment = environment;    
    } 
        public IActionResult Welcome(int page = 1)
     {
        int pageSize = 12;


        List<Product> allProducts = _context.Products.Include(e => e.Associations).ThenInclude(e => e.Category).ToList();

        var totalProducts = allProducts.Count();
        var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

        page = Math.Max(1, Math.Min(page, totalPages));


        var products = allProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();


        List<Category> allCategories = _context.Categories.Include(e => e.Associations).Where(e => e.Associations.Count != 0).ToList();


        var viewModel = new PaginateViewModel
        {
            Products = products,
            PageNumber = page,
            TotalPages = totalPages,
            Categories = allCategories
        };

        return View(viewModel);
    }


        [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("registerr")]   
     public IActionResult CreateUser(Klient newKlient)    
    {        
        if(ModelState.IsValid)        
        {
            // Initializing a PasswordHasher object, providing our User class as its type            
            PasswordHasher<Klient> Hasher = new PasswordHasher<Klient>();   
            // Updating our newUser's password to a hashed version         
            newKlient.Password = Hasher.HashPassword(newKlient, newKlient.Password);            
            //Save your user object to the database 
            _context.Add(newKlient);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("KlientId", newKlient.KlientId);
            return RedirectToAction( "Shop", "Home");       
        } else {
            return View("Register");
        }   
    }
    [HttpGet("logout")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("welcome");
    }


    [HttpPost("login")]
    public IActionResult Login(Login userSubmission)
    {    
        if(ModelState.IsValid)    
        {        
            // If initial ModelState is valid, query for a user with the provided email        
            Klient? klientInDb = _context.Klients.FirstOrDefault(u => u.KlientEmail == userSubmission.LoginEmail);        
            // If no user exists with the provided email        
            if(klientInDb == null)        
            {            
                // Add an error to ModelState and return to RedirectToAction!            
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");            
                return View("register");        
            }   
            // Otherwise, we have a user, now we need to check their password                 
            // Initialize hasher object        
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();                    
            // Verify provided password against hash stored in db        
            var result = hasher.VerifyHashedPassword(userSubmission, klientInDb.Password, userSubmission.LoginPassword);                                    

            if(result == 0)        
            {            
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");            
                return View("register");        
            } 
            
            HttpContext.Session.SetInt32("KlientId", klientInDb.KlientId);
            
            return RedirectToAction( "Shop", "Home");
        } else 
        {
            return View("register");
        }
    }


 

        [HttpGet("shop")]
    public IActionResult Shop(int page = 1)
    {
        int pageSize = 12;


        List<Product> allProducts = _context.Products.Include(e => e.Associations).ThenInclude(e => e.Category).ToList();

        var totalProducts = allProducts.Count();
        var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

        page = Math.Max(1, Math.Min(page, totalPages));


        var products = allProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();


        List<Category> allCategories = _context.Categories.Include(e => e.Associations).Where(e => e.Associations.Count != 0).ToList();


        var viewModel = new PaginateViewModel
        {
            Products = products,
            PageNumber = page,
            TotalPages = totalPages,
            Categories = allCategories
        };

        return View(viewModel);
    }

    [HttpGet("shop/{id}")]
    public IActionResult ShopByCategory(int id)
    {
        PaginateViewModel data = new PaginateViewModel();
        List<Product> AllProducts = _context.Products.Include(e => e.Associations).Where(e => e.Associations.Any(e => e.CategoryId == id)).ToList();
        List<Category> AllCategories = _context.Categories.Include(e => e.Associations).Where(e => e.Associations.Count != 0).ToList();
        data.Products = AllProducts;
        data.Categories = AllCategories;
        return View("Shop", data);
    }

    public IActionResult Privacy()
    {
        ViewBag.AllCategories = _context.Categories.ToList();
        return View();
    }



    [SessionCheck]
    [HttpGet("item/details/{id}")]
    public IActionResult ItemDetails(int id)
    {
        Product product = _context.Products.FirstOrDefault(e => e.ProductId == id);
        DataOne data = new DataOne();
        data.Product = product;
        return View(data);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [SessionCheck]
    [HttpPost("Home/Purchase/{id}")]
    public IActionResult Purchase(Purchase purchase, int id)
    {
        Product product = _context.Products.FirstOrDefault(e => e.ProductId == id);
        DataOne data = new DataOne();
        data.Product = product;
        if (ModelState.IsValid)
        {
            if (product.Quantity < purchase.Quantity || purchase.Quantity < 0)
            {
                ModelState.AddModelError("Quantity", "Not Enough Stock!");

                return View("ItemDetails", data);
            }
            else
            {
                product.Quantity -= purchase.Quantity;
                Klient klient = _context.Klients.FirstOrDefault(e => e.KlientId == HttpContext.Session.GetInt32("KlientId"));
                _context.Add(purchase);
                _context.SaveChanges();
                return RedirectToAction("ItemDetails", new { id = id });
            }
        }
        return View("ItemDetails", data);
    }
    [SessionCheck]
    [HttpGet("myprofile")]
    public IActionResult MyProfile()
    {
        Klient klient = _context.Klients.Include(e => e.Purchases).ThenInclude(e => e.Product).FirstOrDefault(e => e.KlientId == HttpContext.Session.GetInt32("KlientId"));
        return View(klient);
    }

    [SessionCheck]
    [HttpPost("addtocart/{id}")]
    public IActionResult AddToCart(DataOne data, int id)
    {
        

        Product? product = _context.Products.FirstOrDefault(e => e.ProductId == id);
        if (ModelState["Cart.Quantity"].Errors.Count == 0)
        {
            Cart newCart = data.Cart;
            if (_context.Carts.Any(e => e.ProductId == id && e.KlientId == HttpContext.Session.GetInt32("KlientId")))
            {
                Cart actualCart = _context.Carts.FirstOrDefault(e => e.ProductId == id && e.KlientId == HttpContext.Session.GetInt32("KlientId"));
                actualCart.Quantity += newCart.Quantity;
                product.Quantity -= newCart.Quantity;
                _context.SaveChanges();
                return RedirectToAction("ItemDetails", new { id = id });
            }
            product.Quantity -= newCart.Quantity;
            newCart.ProductId = id;
            newCart.KlientId = HttpContext.Session.GetInt32("KlientId");
            Console.WriteLine($"Sasia vjen sa : {newCart.Quantity}");
            int? currentCartNo = HttpContext.Session.GetInt32("CartNo");

            if (currentCartNo.HasValue)
            {
                int newCartNo = currentCartNo.Value + 1;
                HttpContext.Session.SetInt32("CartNo", newCartNo);
                _context.Add(newCart);
                _context.SaveChanges();
                return RedirectToAction("ItemDetails", new { id = id });
            }
            else
            {
                HttpContext.Session.SetInt32("CartNo", 1);
            }
            _context.Add(newCart);
            _context.SaveChanges();
            return RedirectToAction("ItemDetails", new { id = id });
        }
        else
        {
            data.Product = product;
            return View("ItemDetails", data);
        }
    }

    [SessionCheck]
    [HttpGet("mycart")]
    public IActionResult MyCart()
    {
        List<Cart> MyCarts = _context.Carts.Include(e => e.Klient).Include(e => e.Product).Where(e => e.KlientId == HttpContext.Session.GetInt32("UserId")).OrderByDescending(e => e.CartId).ToList();
        return View(MyCarts);
    }


    [SessionCheck]
    [HttpGet("cart/remove/{id}")]
    public IActionResult RemoveFromCart(int id)
    {
        Cart? cart = _context.Carts.FirstOrDefault(e => e.CartId == id);
        Product? product = _context.Products.FirstOrDefault(e => e.ProductId == cart.ProductId);
        product.Quantity += cart.Quantity;
        _context.Remove(cart);
        _context.SaveChanges();
        int? currentCartNo = HttpContext.Session.GetInt32("CartNo");
        if (currentCartNo.HasValue && currentCartNo != 0)
        {
            int newCartNo = currentCartNo.Value - 1;
            HttpContext.Session.SetInt32("CartNo", newCartNo);
        }
        else
        {
            HttpContext.Session.SetInt32("CartNo", 0);
        }

        return RedirectToAction("MyCart");
    }

    [SessionCheck]
    [HttpGet("cart/purchase")]
    public IActionResult BuyFromCart()
    {
        List<Cart>? AllCarts = _context.Carts.Include(e => e.Product).Include(e => e.Klient).Where(e => e.KlientId == HttpContext.Session.GetInt32("KlientId")).ToList();
        foreach (Cart item in AllCarts)
        {
            
            Purchase purchase = new Purchase();
            purchase.Quantity = item.Quantity;
            purchase.Total = item.Product.Price * purchase.Quantity;
            purchase.KlientId = HttpContext.Session.GetInt32("KlientId");
            purchase.ProductId = item.ProductId;
            _context.Purchases.Add(purchase);
            _context.Carts.Remove(item);
        }
        _context.SaveChanges();
        HttpContext.Session.SetInt32("CartNo", 0);
        return RedirectToAction("MyCart");
    }





}






public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("KlientId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Welcome", "Shared", null);
        }
    }
}

public class StoreCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("StoreId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Welcome", "Shared", null);
        }
    }
}
