using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketiIm.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;


namespace MarketiIm.Controllers;


public class StoreController : Controller
{
   private readonly ILogger<StoreController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context; 
    private readonly IWebHostEnvironment _environment;        
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public StoreController(ILogger<StoreController> logger, MyContext context, IWebHostEnvironment environment)    
    {        
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
         _context = context;    
         _environment = environment;
    }

        [HttpGet("sregister")]
    public IActionResult SRegister()
    {
        return View();
    }

    [HttpPost("sregistration")]   
     public IActionResult CreateStore(Store newStore)    
    {        
        if(ModelState.IsValid)        
        {
            // Initializing a PasswordHasher object, providing our User class as its type            
            PasswordHasher<Store> Hasher = new PasswordHasher<Store>();   
            // Updating our newUser's password to a hashed version         
            newStore.Password = Hasher.HashPassword(newStore, newStore.Password);            
            //Save your user object to the database 
            _context.Add(newStore);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("StoreId", newStore.StoreId);
            return RedirectToAction( "SIndex", "Store");       
        } else {
            return View("SRegister");
        }   
    }
        [HttpPost("slogin")]
    public IActionResult SLogin(Login userSubmission)
    {    
        if(ModelState.IsValid)    
        {        
            // If initial ModelState is valid, query for a user with the provided email        
            Store? storeInDb = _context.Stores.FirstOrDefault(u => u.StoreEmail == userSubmission.LoginEmail);        
            // If no user exists with the provided email        
            if(storeInDb == null)        
            {            
                // Add an error to ModelState and return to RedirectToAction!            
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");            
                return View("sregister");        
            }   
            // Otherwise, we have a user, now we need to check their password                 
            // Initialize hasher object        
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();                    
            // Verify provided password against hash stored in db        
            var result = hasher.VerifyHashedPassword(userSubmission, storeInDb.Password, userSubmission.LoginPassword);                                    

            if(result == 0)        
            {            
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");            
                return View("sregister");        
            } 
            
            HttpContext.Session.SetInt32("StoreId", storeInDb.StoreId);
            
            return RedirectToAction( "SIndex", "Store");
        } else 
        {
            return View("sregister");
        }
    }


public IActionResult SIndex(int page = 1)
{
    int pageSize = 10; // Number of items to display per page

    // Retrieve the products with associated categories
    IQueryable<Product> productsQuery = _context.Products.Include(e => e.Associations).ThenInclude(e=> e.Category).Include(e=>e.Purchases).OrderByDescending(e => e.Purchases.Count());;

    

    // Pagination
    var totalProducts = productsQuery.Count();
    var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

    // Ensure the requested page is within the valid range
    page = Math.Max(1, Math.Min(page, totalPages));

    // Apply pagination to the query
    var Products = productsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

    // Pass the paginated and sorted data to the view
    var viewModel = new PaginateViewModel
    {
        Products = Products,
        PageNumber = page,
        TotalPages = totalPages,
    };

    return View(viewModel);
}
    


    
    [HttpGet("newproduct")]
    public IActionResult NewProduct()
    {
        DataTwo DataTwo = new DataTwo();
        DataTwo.Categories= _context.Categories.ToList();
        return View(DataTwo);
    }
    
    
    [HttpPost("registerproduct")]
    public async Task<IActionResult> RegisterProduct(Product product)
{
    if (ModelState.IsValid)
    {
        if (product.ImageFile != null && product.ImageFile.Length > 0)
        {
            Console.WriteLine("U ekzekutua");
            // Process the uploaded file
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(fileStream);
            }

            // Update the model properties with the file details
            product.ImageFileName = uniqueFileName;
            product.ImageData = System.IO.File.ReadAllBytes(filePath);
        }
            _context.Products.Add(product);
            _context.SaveChanges();
            Association association = new Association();
             association.ProductId = product.ProductId;
            association.CategoryId = product.CategoryId;
            _context.Associations.Add(association);
            _context.SaveChanges();
            return RedirectToAction("SIndex");
           }
   
         ViewBag.AllProducts = _context.Products.ToList();
        DataTwo DataTwo = new DataTwo();
        DataTwo.Categories= _context.Categories.ToList();
        return View("NewProduct", DataTwo);

}

    

[HttpGet("delete/{id}")]
public IActionResult DeleteItem(int id)
{
    Product product = _context.Products.Include(e => e.Associations).FirstOrDefault(e => e.ProductId == id);
    List<Association> associations = _context.Associations.Where(e => e.ProductId == id).ToList();
    List<Purchase> purchases = _context.Purchases.Where(p => p.ProductId == id).ToList();
    List<Cart> carts = _context.Carts.Where(p => p.ProductId == id).ToList();
    _context.Purchases.RemoveRange(purchases);
     _context.Carts.RemoveRange(carts);
    _context.Associations.RemoveRange(associations);
    
    _context.Products.Remove(product);

    _context.SaveChanges();

    return RedirectToAction("Index");
}


    [HttpGet("item/edit/{id}")]
    public IActionResult EditItem(int id)
    {
        DataTwo DataTwo = new DataTwo();
        DataTwo.Categories= _context.Categories.ToList();
        DataTwo.Product = _context.Products.FirstOrDefault(e => e.ProductId == id);
        return View(DataTwo);
    }


[HttpPost("item/postedit/{id}")]
public async Task<IActionResult> EditProduct(DataTwo data, int id)
{
    if (data.Product == null)
    {
  
        Console.WriteLine("futja kot");
    }
    if (ModelState.IsValid)
    {
 
        Product productFromDb = _context.Products.Include(e => e.Associations).ThenInclude(e => e.Category).FirstOrDefault(e => e.ProductId == id);

    
        productFromDb.Name = data.Product.Name;
        productFromDb.Brand = data.Product.Brand;
        productFromDb.Price = data.Product.Price;
        productFromDb.Quantity = data.Product.Quantity;
        productFromDb.Description = data.Product.Description;
        productFromDb.UpdatedAt = DateTime.Now;


        Association assoc = productFromDb.Associations.FirstOrDefault(e => e.ProductId == productFromDb.ProductId);
        if (assoc != null)
        {
            assoc.CategoryId = data.Product.CategoryId;
        }
        else
        {
 
            Association newAssociation = new Association
            {
                ProductId = productFromDb.ProductId,
                CategoryId = data.Product.CategoryId
            };
            productFromDb.Associations.Add(newAssociation);
        }

        // Check if a new image file is uploaded
        if (data.Product.ImageFile != null && data.Product.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + data.Product.ImageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await data.Product.ImageFile.CopyToAsync(fileStream);
            }

            productFromDb.ImageFileName = uniqueFileName;
            productFromDb.ImageData = await System.IO.File.ReadAllBytesAsync(filePath);
        }

        await _context.SaveChangesAsync(); 

        return RedirectToAction("Index");
    }

    data.Categories= _context.Categories.ToList();
    return View("EditItem", data);
}



    [HttpPost("registercategory")]
    public IActionResult RegisterCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction("NewProduct");
        }
        ViewBag.AllCategories = _context.Categories.ToList();
        DataTwo DataTwo = new DataTwo();
        DataTwo.Categories= _context.Categories.ToList();
        return View("NewProduct",DataTwo);
    }

 




[HttpGet("showpurchases")]
public IActionResult ShowPurchases(int page = 1)
{
    int pageSize = 10; 
    IQueryable<Purchase> purchasesQuery = _context.Purchases.Include(e => e.Product).Include(e => e.Klient).OrderByDescending(e => e.PurchaseId);


    var totalPurchases = purchasesQuery.Count();
    var totalPages = (int)Math.Ceiling((double)totalPurchases / pageSize);

  
    page = Math.Max(1, Math.Min(page, totalPages));


    var purchases = purchasesQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();


    var viewModel = new PaginateViewModel
    {
        Purchases = purchases,
        PageNumber = page,
        TotalPages = totalPages,
    };

    return View(viewModel);
}


}