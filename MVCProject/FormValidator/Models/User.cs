namespace FormValidator.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Required(ErrorMessage = "Name is required!")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters in length.")]

    public string name { get; set; }

    [Required(ErrorMessage = "Email is required!")]
    [EmailValid]
    public string email { get; set; }

    [Required(ErrorMessage = "Date is required!")]
    [Skadenca]
    public DateTime dateTime { get; set; }

    [Required(ErrorMessage = "Password is required is required!")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters in length.")]
    public string password { get; set; }

    [Required(ErrorMessage = "Number is required!")]
    [PrimeNumber]
    public int number { get; set; }
    
}


public class SkadencaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime currentDate = DateTime.Now;
        if (currentDate < (DateTime)value)
        {
            return new ValidationResult("Nuk lejohet nje date qe eshte ne te ardhmen");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}


public class EmailValidAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if((string)value==null){
            return new ValidationResult("Adresa e email duhet plotesuar");
        }
        int c = 0;
        for (int i = 0; i < ((string)value).Length; i++)
        {
            if (((string)value).ToLower()[i] == '@')
            {
                c++;
            }
            else if (((string)value).ToLower()[i] == '.')
            {
                c++;
            }
        }

        if (c != 2)
        {
            return new ValidationResult("Adresa e email duhet te permbaje nje @ dhe nje .");
        }
        else
        {
            return ValidationResult.Success;
        }

    }
}




public class PrimeNumberAttribute : ValidationAttribute
{
protected override ValidationResult IsValid(object value, ValidationContext validationContext)
{
    int number = (int)value;

    if (number <= 1)
    {
        return new ValidationResult("Numri duhet me i madh se 1.");
    }

    bool isPrime = true;

    for (int i = 2; i <= Math.Sqrt(number); i++)
    {
        if (number % i == 0)
        {
            isPrime = false; // If a divisor is found, it's not a prime number
            break; // Break the loop as we already found a divisor
        }
    }

    if (!isPrime)
    {
        return new ValidationResult("Numri nuk eshte i thjesht.");
    }
    else
    {
        return ValidationResult.Success;
    }
}
}



