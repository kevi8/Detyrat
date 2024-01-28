#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MarketiIm.Models;


public class Store
{
    [Key]
    public int StoreId { get; set; }


    [Required(ErrorMessage ="First Name is required")]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters")]
    public string Name {get; set;}



    

    [Required(ErrorMessage ="Address is required")]
    [MinLength(2, ErrorMessage = "Address must be at least 2 characters")]
    public string Adressa {get; set;}


    [Required]
    [EmailAddress]
    [UniqueKlientEmail]
    public string StoreEmail { get; set; }


    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; }    

    [NotMapped]
    [DataType(DataType.Password)]
    [Display(Name ="Confirm Password")]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Product> Products { get; set; }
}


//Stackoverflow Unique Email Code
public class UniqueStoreEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
    	// Though we have Required as a validation, sometimes we make it here anyways
    	// In which case we must first verify the value is not null before we proceed
        if(value == null)
        {
    	    // If it was, return the required error
            return new ValidationResult("Email is required!");
        }
    
    	// This will connect us to our database since we are not in our Controller
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        // Check to see if there are any records of this email in our database
        if(_context.Stores.Any(e => e.StoreEmail == value.ToString()))
        {
    	    // If yes, throw an error
            return new ValidationResult("Email must be unique!");
        } else {
    	    // If no, proceed
            return ValidationResult.Success;
        }
    }
}