#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MarketiIm.Models;

public class PaginateViewModel
{
    public List<Product> Products { get; set; }
    public List<Klient> Klients {get;set;}
    public List<Store> Stores {get;set;}
    public List<Purchase> Purchases {get;set;}
    public List<Category> Categories {get;set;}
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}