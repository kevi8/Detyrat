#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Planer.Models;

public class Plan
{    
    [Key]
    public int PlanId { get; set; }
    // Each property in a model gets its own set of DataAnnotations
    // Each annotation applies only to the property that is directly below it
    [Required]   
    // We can stack annotations to apply multiple requirements to one property
    // In this case, a Username is required but must also be at least 3 characters long 
       
        
    // Notice how we must use [Required] again here to apply to the next property
    [MinLength(3)] 
    public string WeederOne { get; set; }
    [MinLength(3)] 
    public string WeederTwo { get; set; }
    public int? UserId {get;set;}
    public string Guest {get;set;}
    [Required]
    public string Address { get; set; }

       
    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
    public DateTime End { get; set; }
   
}