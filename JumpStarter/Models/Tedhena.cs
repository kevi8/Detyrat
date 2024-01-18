#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using JumpStarter.Models;
namespace JumpStarter.Models;

public class Tedhena
{
    
    public Project? Project{get;set;}
    public List<Project>? Projects{get;set;}
    public UserAndProject? UserAndProject{get;set;}
    public int SupportersCount { get; set; }
    public decimal TotalDonations { get; set; }
    public TimeSpan UntilEnd { get; set; }
}