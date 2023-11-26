using Microsoft.AspNetCore.Mvc;
namespace Countdown.Controllers;   
public class HelloController : Controller     
{      
    [HttpGet("")]
    public ViewResult Index()        
    {            
    	return View();        
    }    
}