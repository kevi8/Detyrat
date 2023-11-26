// This brings all the MVC features we need to this file
using Microsoft.AspNetCore.Mvc;
// Be sure to use your own project's namespace here! 
namespace YourNamespace.Controllers;   
public class HelloController : Controller   // Remember inheritance?    
{      
    [HttpGet] // We will go over this in more detail on the next page    
    [Route("")] // We will go over this in more detail on the next page
    public string Index()        
    {            
    	return Index();      
    }    
    [HttpPost]
    public ActionResult Index(FormData formData)
    {
        // Render the data on a results page
        return View("Results", formData);
    }
}

