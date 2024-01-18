// Don't forget to disable the warnings!
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Profile.Models;
public class Net
{  
 
    [Key]
    public int NetId { get; set; }
     public int? UserId {get;set;}
    [Required]
    public string Name { get; set; } 
    [MinLength(4,ErrorMessage = "pershkrimi me i gjate se 4 shkronja")]
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public User? Creator {get;set;}
    public List<Ftesa>? ftesat {get;set;}
    //public List<Connected>? Connecteds {get;set;}
}