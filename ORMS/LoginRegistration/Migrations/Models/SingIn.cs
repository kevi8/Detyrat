#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginRegistration.Models;
public class SingIn
{  
    
    [Required]   
    // This will apply a standard Email format regex to this property 
    [EmailAddress]    
    public string SEmail { get; set; }     
    
    [Required]    
    // You will see what the [DataType] annotation does when we get over to making our form
    [DataType(DataType.Password)]    
    public string SPassword { get; set; }
}
