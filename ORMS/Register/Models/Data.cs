#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Register.Models;

public class Data{
    public Post post {get;set;}

    public Comment comment {get;set;}
}