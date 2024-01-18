// Don't forget to disable the warnings!
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Profile.Models;
public class Ftesa
{    
    [Key]
    public int FtesaId { get; set; }
     
        
    // Notice how we must use [Required] again here to apply to the next property
    public int? UserId {get;set;}
    public User? IFtuari {get;set;}
    public int? NetId {get;set;}
    public Net? Nets {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;        
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
   
}