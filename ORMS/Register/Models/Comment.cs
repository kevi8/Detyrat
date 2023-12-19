#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Register.Models;
public class Comment
{    
    [Key]
    public int CommentId { get; set; }
    // Each property in a model gets its own set of DataAnnotations
    // Each annotation applies only to the property that is directly below it
    [Required]   
    // We can stack annotations to apply multiple requirements to one property
    // In this case, a Username is required but must also be at least 3 characters long 
    [MinLength(1)]    
    public string Content { get; set; } 

    public int? UserId {get;set;}
    public int? PostId {get;set;}
    public Post? Post {get;set;}
    public User? User {get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.Now;

}