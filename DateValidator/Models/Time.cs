namespace DateValidator.Models;
using System.ComponentModel.DataAnnotations;

public class Time
{
    [Skadenca]
    public DateTime? dateTime { get; set; }
}


public class SkadencaAttribute : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime currentDate = DateTime.Now;
        if (currentDate < (DateTime)value)
        {
            return new ValidationResult("Data esh ne te ardhmen");
        } else {
            return ValidationResult.Success;
        }
    }
}
