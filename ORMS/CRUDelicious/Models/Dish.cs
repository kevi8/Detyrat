#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    public string Name { get; set; } 
    public string Chef { get; set; } 
    [Range (1,6, ErrorMessage = "Duhet te jete midis 1 dhe 5")]
    public int Testines { get; set; }
    public int Calories { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
                
