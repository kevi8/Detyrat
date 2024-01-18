#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration.UserSecrets;
namespace MovieStore.Models;
public class Login
{    
 
    // Each property in a model gets its own set of DataAnnotations
    // Each annotation applies only to the property that is directly below it
       // This will apply a standard Email format regex to this property 
    // Notice how we must use [Required] again here to apply to the next property
      
    
    [Required]    
    public string? Username { get; set; }     
    
    [Required]    
    // You will see what the [DataType] annotation does when we get over to making our form
    [DataType(DataType.Password)]    
    public string? Password { get; set; } 
    
}
